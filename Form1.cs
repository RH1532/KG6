using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KG6
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

        private bool _showThreads;
        private bool _isParallel;
        private int _degreeOfParallelism = Environment.ProcessorCount;
        private CancellationTokenSource _cancellation;

        private int _width, _height;
        private Bitmap _bitmap;
        private Rectangle _rect;
        private ObjectPool<int[]> _freeBuffers;

        private void OnStartButtonClick(object sender, EventArgs e)
        {
            if (_cancellation != null)
            {
                _startButton.Enabled = false;
                _cancellation.Cancel();
            }
            else
            {
                ConfigureImage();
                _cancellation = new CancellationTokenSource();
                Task.Factory.StartNew(RenderLoop,
                    _cancellation.Token, _cancellation.Token)
                    .ContinueWith(antedecent =>
                    {
                        _startButton.Enabled = true;
                        _startButton.Text = "Start";
                        _cancellation = null;
                    }, TaskScheduler.FromCurrentSynchronizationContext());

                _startButton.Text = "Stop";
            }
        }

        private void ConfigureImage()
        {
            if (_bitmap == null || _bitmap.Width != _renderedImage.Width || _bitmap.Height != _renderedImage.Height)
            {
                if (_bitmap != null)
                {
                    _renderedImage.Image = null;
                    _bitmap.Dispose();
                }

                _width = _height = Math.Min(_renderedImage.Width, _renderedImage.Height);

                _freeBuffers = new ObjectPool<int[]>(() => new int[_width * _height]);

                _bitmap = new Bitmap(_width, _height, PixelFormat.Format32bppRgb);
                _rect = new Rectangle(0, 0, _width, _height);
                _renderedImage.Image = _bitmap;
            }
        }

        private void RenderLoop(object boxedToken)
        {
            var cancellationToken = (CancellationToken)boxedToken;

            var rayTracer = new RayTracer(_width, _height);
            var scene = rayTracer._defaultScene;
            var sphere2 = (Sphere)scene.Things[0];
            var baseY = sphere2.Radius;
            sphere2.Center.Y = sphere2.Radius;

            var renderingTime = new Stopwatch();
            var totalTime = Stopwatch.StartNew();

            while (!cancellationToken.IsCancellationRequested)
            {
                var rgb = _freeBuffers.GetObject();

                double dy2 = 0.8 * Math.Abs(Math.Sin(totalTime.ElapsedMilliseconds * Math.PI / 3000));
                sphere2.Center.Y = baseY + dy2;

                renderingTime.Reset();
                renderingTime.Start();

                var options = new ParallelOptions
                {
                    MaxDegreeOfParallelism = _degreeOfParallelism,
                    CancellationToken = _cancellation.Token
                };
                if (!_isParallel)
                {
                    rayTracer.RenderSequential(scene, rgb);
                }
                else if (_showThreads)
                {
                    rayTracer.RenderParallelShowingThreads(scene, rgb, options);
                }
                else
                {
                    rayTracer.RenderParallel(scene, rgb, options);
                }

                renderingTime.Stop();

                var framesPerSecond = 1000.0 / renderingTime.ElapsedMilliseconds;
                BeginInvoke((Action)delegate
                {
                    var bmpData = _bitmap.LockBits(_rect, ImageLockMode.WriteOnly, _bitmap.PixelFormat);
                    Marshal.Copy(rgb, 0, bmpData.Scan0, rgb.Length);
                    _bitmap.UnlockBits(bmpData);
                    _freeBuffers.PutObject(rgb);

                    _renderedImage.Invalidate();
                    Text = $"Ray Tracer - FPS: {framesPerSecond:F1}";
                });
            }
        }
    }
}