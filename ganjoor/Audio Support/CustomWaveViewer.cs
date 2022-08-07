using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;

namespace ganjoor
{
    /// <summary>
    /// Control for viewing waveforms
    /// </summary>
    public class CustomWaveViewer : UserControl
    {
        public Color PenColor { get; set; }
        public float PenWidth { get; set; }

        public void FitToScreen()
        {
            if (waveStream == null) return;

            var samples = (int)(waveStream.Length / bytesPerSample);
            startPosition = 0;
            SamplesPerPixel = samples / Width;
            PreparePaintData();
            Invalidate();

        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _CurrentPosition = e.X;
                OnPositionChanged?.Invoke(this, new EventArgs());

                Invalidate();

            }

            base.OnMouseDown(e);
        }

        private int _CurrentPosition;

        public int Position
        {
            set
            {
                if (waveStream != null)
                {
                    var f = (float)(value / waveStream.TotalTime.TotalMilliseconds);
                    _CurrentPosition = (int)(f * Width);
                    Invalidate();
                }
            }
            get
            {
                return _CurrentPosition;
            }
        }

        public event EventHandler OnPositionChanged;






        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            FitToScreen();
        }

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private Container components;
        private WaveStream waveStream;
        private int samplesPerPixel = 128;
        private long startPosition;
        private int bytesPerSample;
        /// <summary>
        /// Creates a new WaveViewer control
        /// </summary>
        public CustomWaveViewer()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            DoubleBuffered = true;

            PenColor = Color.DodgerBlue;
            PenWidth = 1;
        }

        /// <summary>
        /// sets the associated wavestream
        /// </summary>
        public WaveStream WaveStream
        {
            get
            {
                return waveStream;
            }
            set
            {
                waveStream = value;
                if (waveStream != null)
                {
                    bytesPerSample = waveStream.WaveFormat.BitsPerSample / 8 * waveStream.WaveFormat.Channels;
                }
                //this.Invalidate();
            }
        }

        /// <summary>
        /// The zoom level, in samples per pixel
        /// </summary>
        public int SamplesPerPixel
        {
            get
            {
                return samplesPerPixel;
            }
            set
            {
                samplesPerPixel = Math.Max(1, value);
                Invalidate();
            }
        }

        /// <summary>
        /// Start position (currently in bytes)
        /// </summary>
        public long StartPosition
        {
            get
            {
                return startPosition;
            }
            set
            {
                startPosition = value;
            }
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private PointF[] _PrepareData;

        private void PreparePaintData()
        {

            if (waveStream != null)
            {
                var StartX = 0;
                var EndX = Width;
                _PrepareData = new PointF[EndX];
                waveStream.Position = 0;
                int bytesRead;
                var waveData = new byte[samplesPerPixel * bytesPerSample];
                waveStream.Position = startPosition + StartX * bytesPerSample * samplesPerPixel;



                for (var x = StartX; x < EndX; x++)
                {
                    short low = 0;
                    short high = 0;
                    bytesRead = waveStream.Read(waveData, 0, samplesPerPixel * bytesPerSample);
                    if (bytesRead == 0)
                        break;

                    for (var n = 0; n < bytesRead; n += 2)
                    {
                        var sample = BitConverter.ToInt16(waveData, n);
                        if (sample < low) low = sample;
                        if (sample > high) high = sample;
                    }
                    var lowPercent = ((float)low - short.MinValue) / ushort.MaxValue;
                    var highPercent = ((float)high - short.MinValue) / ushort.MaxValue;
                    _PrepareData[x] = new PointF(lowPercent, highPercent);
                }
            }

        }


        /// <summary>
        /// <see cref="Control.OnPaint"/>
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_PrepareData != null)
            {
                using (var linePen = new Pen(PenColor, PenWidth))
                {
                    for (var x = e.ClipRectangle.X; x < e.ClipRectangle.Right; x++)
                    {
                        if (x >= _PrepareData.Length)
                            break;
                        e.Graphics.DrawLine(linePen, x, Height * _PrepareData[x].X, x, Height * _PrepareData[x].Y);
                    }
                }

                e.Graphics.DrawLine(Pens.Black, _CurrentPosition, 0, _CurrentPosition, Height);

            }

            base.OnPaint(e);
        }


        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion
    }
}