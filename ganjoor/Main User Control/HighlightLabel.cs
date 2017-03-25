﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using ganjoor.Properties;

namespace ganjoor
{
    class HighlightLabel : Label
    {
        #region Constructor
        public HighlightLabel() :
            this(string.Empty, Settings.Default.HighlightColor)
        {
        }
        public HighlightLabel(string keyword, Color highlightColor)
        {
            Keyword = keyword;
            HighlightColor = highlightColor;
        }
        #endregion
        #region Properties
        public string Keyword { set; get; }
        public Color HighlightColor { set; get; }
        #endregion
        #region Paint
        protected override void OnPaint(PaintEventArgs e)
        {
                       
            if (!
                (
                string.IsNullOrEmpty(Keyword)
                ||
                this.Text.IndexOf(Keyword) == -1
                )
               )
            {
                string txt = this.Text;
                using (SolidBrush hbrsh = new SolidBrush(HighlightColor))
                {
                    float fx = e.ClipRectangle.Right;//only right to left text for now
                    float fy = e.ClipRectangle.Y;
                    while (txt.Length > 0)
                    {
                        int index = txt.IndexOf(Keyword);
                        string thisPart;
                        if (index == -1)
                        {
                            thisPart = txt;
                            txt = string.Empty;
                        }
                        else
                        {
                            if (index == 0)
                            {
                                thisPart = txt.Substring(0, Keyword.Length);
                                if (txt == Keyword)
                                    txt = string.Empty;
                                else
                                    txt = txt.Substring(Keyword.Length, txt.Length - Keyword.Length);

                            }
                            else
                            {
                                thisPart = txt.Substring(0, index);
                                txt = txt.Substring(index);
                            }
                        }
                        SizeF sz = TextRenderer.MeasureText(thisPart, this.Font, Size.Empty, TextFormatFlags.TextBoxControl | TextFormatFlags.RightToLeft);
                        if (index == 0)
                        {
                            e.Graphics.FillRectangle(hbrsh, new RectangleF(fx - sz.Width, fy, sz.Width, sz.Height));
                        }
                        fx -= sz.Width;
                        if (fx <= 0)
                        {//multiline label
                            int nLines = (int)sz.Width / this.Width;
                            fy += nLines * sz.Height;
                            fx = e.ClipRectangle.Right - (sz.Width - nLines * this.Width);
                        }
                    }
                }
            }
            base.OnPaint(e);
        }
        #endregion
    }
}
