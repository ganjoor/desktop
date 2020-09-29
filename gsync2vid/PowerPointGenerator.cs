using System;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using P = DocumentFormat.OpenXml.Presentation;
using A = DocumentFormat.OpenXml.Drawing;
using System.Diagnostics;

namespace gsync2vid
{
    public class PowerPointGenerator
    {
        #region PowerPoint Generation
        public void CreatePresentation(string filepath, GVideoFrame[] frames)
        {
            //Sample Source: https://docs.microsoft.com/en-us/office/open-xml/how-to-create-a-presentation-document-by-providing-a-file-name
            // Create a presentation at a specified file path. The presentation document type is pptx, by default.
            PresentationDocument presentationDoc = PresentationDocument.Create(filepath, PresentationDocumentType.Presentation);
            PresentationPart presentationPart = presentationDoc.AddPresentationPart();
            presentationPart.Presentation = new P.Presentation();

            CreatePresentationParts(presentationPart);

            int i = 0;
            foreach (GVideoFrame frame in frames)
            {
                i += InsertNewSlide(presentationDoc, i, frame);
            }


            // Close the presentation handle
            presentationDoc.Close();

            Process.Start(filepath);
        }

        // Insert the specified slide into the presentation at the specified position.
        public static int InsertNewSlide(PresentationDocument presentationDocument, int position, GVideoFrame frame)
        {
            //if (frame.MasterFrame != null)
            //    return 0;
            if (presentationDocument == null)
            {
                throw new ArgumentNullException("presentationDocument");
            }

           

            PresentationPart presentationPart = presentationDocument.PresentationPart;

            // Verify that the presentation is not empty.
            if (presentationPart == null)
            {
                throw new InvalidOperationException("The presentation document is empty.");
            }

            // Declare and instantiate a new slide.
            P.Slide slide = new P.Slide(new P.CommonSlideData(new P.ShapeTree()));
            uint drawingObjectId = 1;

            // Construct the slide content.            
            // Specify the non-visual properties of the new slide.
            P.NonVisualGroupShapeProperties nonVisualProperties = slide.CommonSlideData.ShapeTree.AppendChild(new P.NonVisualGroupShapeProperties());
            nonVisualProperties.NonVisualDrawingProperties = new P.NonVisualDrawingProperties() { Id = 1, Name = "" };
            nonVisualProperties.NonVisualGroupShapeDrawingProperties = new P.NonVisualGroupShapeDrawingProperties();
            nonVisualProperties.ApplicationNonVisualDrawingProperties = new P.ApplicationNonVisualDrawingProperties();

            // Specify the group shape properties of the new slide.
            slide.CommonSlideData.ShapeTree.AppendChild(new P.GroupShapeProperties());


            // Declare and instantiate the title shape of the new slide.
            P.Shape titleShape = slide.CommonSlideData.ShapeTree.AppendChild(new P.Shape());

            drawingObjectId++;

            // Specify the required shape properties for the title shape. 
            titleShape.NonVisualShapeProperties = new P.NonVisualShapeProperties
                (new P.NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Title" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape() { Type = P.PlaceholderValues.Title }));
            titleShape.ShapeProperties = new P.ShapeProperties();

            titleShape.ShapeProperties = new P.ShapeProperties()
            {
                Transform2D = new A.Transform2D()
                {
                    Offset = new A.Offset() { X = 1335696L, Y = 3036712L },
                    Extents = new A.Extents() { Cx = 6334617, Cy = 1025963 }

                }
            };

       




            // Specify the text of the title shape.
            A.Run run = new A.Run();
            A.RunProperties runProperties = new A.RunProperties() { Language = "fa-IR", FontSize = (int)(frame.Font.Size * 100), Dirty = false };//Set Font-Size to 60px.
            A.LatinFont latinFont10 = new A.LatinFont() { Typeface = frame.Font.Name, Panose = "02040503050201020203", PitchFamily = 18, CharacterSet = -78 };
            A.ComplexScriptFont complexScriptFont10 = new A.ComplexScriptFont() { Typeface = frame.Font.Name, Panose = "02040503050201020203", PitchFamily = 18, CharacterSet = -78 };


            A.Text text = new A.Text() { Text = frame.Text };

            
            run.Append(runProperties);

            A.Paragraph paragraph1 = new A.Paragraph();
            A.ParagraphProperties paragraphProperties1 = new A.ParagraphProperties() { Alignment = A.TextAlignmentTypeValues.Center };

            
            run.Append(text);

            runProperties.Append(latinFont10);
            runProperties.Append(complexScriptFont10);
            paragraph1.Append(paragraphProperties1);
            paragraph1.Append(run);

            titleShape.TextBody = new P.TextBody(new A.BodyProperties(),
                    new A.ListStyle(),
                    paragraph1);

           
            // Declare and instantiate the body shape of the new slide.
            P.Shape bodyShape = slide.CommonSlideData.ShapeTree.AppendChild(new P.Shape());
            drawingObjectId++;

            // Specify the required shape properties for the body shape.
            bodyShape.NonVisualShapeProperties = new P.NonVisualShapeProperties(
                    new P.NonVisualDrawingProperties() { Id = drawingObjectId, Name = "Content Placeholder" },
                    new P.NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                    new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape() { Index = 1 }));
            bodyShape.ShapeProperties = new P.ShapeProperties();

            // Specify the text of the body shape.
            bodyShape.TextBody = new P.TextBody(new A.BodyProperties(),
                    new A.ListStyle(),
                    new A.Paragraph());


            // Create the slide part for the new slide.
            SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();

            // Save the new slide part.
            slide.Save(slidePart);

            // Modify the slide ID list in the presentation part.
            // The slide ID list should not be null.
            P.SlideIdList slideIdList = presentationPart.Presentation.SlideIdList;

            // Find the highest slide ID in the current list.
            uint maxSlideId = 1;
            P.SlideId prevSlideId = null;

            foreach (P.SlideId slideId in slideIdList.ChildElements)
            {
                if (slideId.Id > maxSlideId)
                {
                    maxSlideId = slideId.Id;
                }

                position--;
                if (position == 0)
                {
                    prevSlideId = slideId;
                }

            }

            maxSlideId++;

            // Get the ID of the previous slide.
            SlidePart lastSlidePart;

            if (prevSlideId != null)
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(prevSlideId.RelationshipId);
            }
            else
            {
                lastSlidePart = (SlidePart)presentationPart.GetPartById(((P.SlideId)(slideIdList.ChildElements[0])).RelationshipId);
            }

            // Use the same slide layout as that of the previous slide.
            if (null != lastSlidePart.SlideLayoutPart)
            {
                slidePart.AddPart(lastSlidePart.SlideLayoutPart);
            }

            // Insert the new slide into the slide list after the previous slide.
            P.SlideId newSlideId = slideIdList.InsertAfter(new P.SlideId(), prevSlideId);
            newSlideId.Id = maxSlideId;
            newSlideId.RelationshipId = presentationPart.GetIdOfPart(slidePart);

            // Save the modified presentation.
            presentationPart.Presentation.Save();

            return 1;
        }



        private void CreatePresentationParts(PresentationPart presentationPart)
        {
            P.SlideMasterIdList slideMasterIdList1 = new P.SlideMasterIdList(new P.SlideMasterId() { Id = (UInt32Value)2147483648U, RelationshipId = "rId1" });
            P.SlideIdList slideIdList1 = new P.SlideIdList(new P.SlideId() { Id = (UInt32Value)256U, RelationshipId = "rId2" });
            P.SlideSize slideSize1 = new P.SlideSize() { Cx = 9144000, Cy = 6858000, Type = P.SlideSizeValues.Screen4x3 };
            P.NotesSize notesSize1 = new P.NotesSize() { Cx = 6858000, Cy = 9144000 };
            P.DefaultTextStyle defaultTextStyle1 = new P.DefaultTextStyle();

            presentationPart.Presentation.Append(slideMasterIdList1, slideIdList1, slideSize1, notesSize1, defaultTextStyle1);

            SlidePart slidePart1;
            SlideLayoutPart slideLayoutPart1;
            SlideMasterPart slideMasterPart1;
            ThemePart themePart1;


            slidePart1 = CreateSlidePart(presentationPart);
            slideLayoutPart1 = CreateSlideLayoutPart(slidePart1);
            slideMasterPart1 = CreateSlideMasterPart(slideLayoutPart1);
            themePart1 = CreateTheme(slideMasterPart1);

            slideMasterPart1.AddPart(slideLayoutPart1, "rId1");
            presentationPart.AddPart(slideMasterPart1, "rId1");
            presentationPart.AddPart(themePart1, "rId5");
        }

        private SlidePart CreateSlidePart(PresentationPart presentationPart)
        {
            SlidePart slidePart1 = presentationPart.AddNewPart<SlidePart>("rId2");
            slidePart1.Slide = new P.Slide(
                    new P.CommonSlideData(
                        new P.ShapeTree(
                            new P.NonVisualGroupShapeProperties(
                                new P.NonVisualDrawingProperties() { Id = (UInt32Value)1U, Name = "" },
                                new P.NonVisualGroupShapeDrawingProperties(),
                                new P.ApplicationNonVisualDrawingProperties()),
                            new P.GroupShapeProperties(new A.TransformGroup()),
                            new P.Shape(
                                new P.NonVisualShapeProperties(
                                    new P.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Title 1" },
                                    new P.NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                                    new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape())),
                                new P.ShapeProperties(),
                                new P.TextBody(
                                    new A.BodyProperties(),
                                    new A.ListStyle(),
                                    new A.Paragraph(new A.EndParagraphRunProperties() { Language = "fa-IR" }))))),
                    new P.ColorMapOverride(new A.MasterColorMapping()));
            return slidePart1;
        }

        private SlideLayoutPart CreateSlideLayoutPart(SlidePart slidePart1)
        {
            SlideLayoutPart slideLayoutPart1 = slidePart1.AddNewPart<SlideLayoutPart>("rId1");
            P.SlideLayout slideLayout = new P.SlideLayout(
            new P.CommonSlideData(new P.ShapeTree(
              new P.NonVisualGroupShapeProperties(
              new P.NonVisualDrawingProperties() { Id = (UInt32Value)1U, Name = "" },
              new P.NonVisualGroupShapeDrawingProperties(),
              new P.ApplicationNonVisualDrawingProperties()),
              new P.GroupShapeProperties(new A.TransformGroup()),
              new P.Shape(
              new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape())),
              new P.ShapeProperties(),
              new P.TextBody(
                new A.BodyProperties(),
                new A.ListStyle(),
                new A.Paragraph(new A.EndParagraphRunProperties()))))),
            new P.ColorMapOverride(new A.MasterColorMapping()));
            slideLayoutPart1.SlideLayout = slideLayout;
            return slideLayoutPart1;
        }

        private SlideMasterPart CreateSlideMasterPart(SlideLayoutPart slideLayoutPart1)
        {
            SlideMasterPart slideMasterPart1 = slideLayoutPart1.AddNewPart<SlideMasterPart>("rId1");
            P.SlideMaster slideMaster = new P.SlideMaster(
            new P.CommonSlideData(new P.ShapeTree(
              new P.NonVisualGroupShapeProperties(
              new P.NonVisualDrawingProperties() { Id = (UInt32Value)1U, Name = "" },
              new P.NonVisualGroupShapeDrawingProperties(),
              new P.ApplicationNonVisualDrawingProperties()),
              new P.GroupShapeProperties(new A.TransformGroup()),
              new P.Shape(
              new P.NonVisualShapeProperties(
                new P.NonVisualDrawingProperties() { Id = (UInt32Value)2U, Name = "Title Placeholder 1" },
                new P.NonVisualShapeDrawingProperties(new A.ShapeLocks() { NoGrouping = true }),
                new P.ApplicationNonVisualDrawingProperties(new P.PlaceholderShape() { Type = P.PlaceholderValues.Title })),
              new P.ShapeProperties(),
              new P.TextBody(
                new A.BodyProperties(),
                new A.ListStyle(),
                new A.Paragraph())))),
            new P.ColorMap() { Background1 = A.ColorSchemeIndexValues.Light1, Text1 = A.ColorSchemeIndexValues.Dark1, Background2 = A.ColorSchemeIndexValues.Light2, Text2 = A.ColorSchemeIndexValues.Dark2, Accent1 = A.ColorSchemeIndexValues.Accent1, Accent2 = A.ColorSchemeIndexValues.Accent2, Accent3 = A.ColorSchemeIndexValues.Accent3, Accent4 = A.ColorSchemeIndexValues.Accent4, Accent5 = A.ColorSchemeIndexValues.Accent5, Accent6 = A.ColorSchemeIndexValues.Accent6, Hyperlink = A.ColorSchemeIndexValues.Hyperlink, FollowedHyperlink = A.ColorSchemeIndexValues.FollowedHyperlink },
            new P.SlideLayoutIdList(new P.SlideLayoutId() { Id = (UInt32Value)2147483649U, RelationshipId = "rId1" }),
            new P.TextStyles(new P.TitleStyle(), new P.BodyStyle(), new P.OtherStyle()));
            slideMasterPart1.SlideMaster = slideMaster;

            return slideMasterPart1;
        }

        private ThemePart CreateTheme(SlideMasterPart slideMasterPart1)
        {
            ThemePart themePart1 = slideMasterPart1.AddNewPart<ThemePart>("rId5");
            A.Theme theme1 = new A.Theme() { Name = "Office Theme" };

            A.ThemeElements themeElements1 = new A.ThemeElements(
            new A.ColorScheme(
              new A.Dark1Color(new A.SystemColor() { Val = A.SystemColorValues.WindowText, LastColor = "000000" }),
              new A.Light1Color(new A.SystemColor() { Val = A.SystemColorValues.Window, LastColor = "FFFFFF" }),
              new A.Dark2Color(new A.RgbColorModelHex() { Val = "1F497D" }),
              new A.Light2Color(new A.RgbColorModelHex() { Val = "EEECE1" }),
              new A.Accent1Color(new A.RgbColorModelHex() { Val = "4F81BD" }),
              new A.Accent2Color(new A.RgbColorModelHex() { Val = "C0504D" }),
              new A.Accent3Color(new A.RgbColorModelHex() { Val = "9BBB59" }),
              new A.Accent4Color(new A.RgbColorModelHex() { Val = "8064A2" }),
              new A.Accent5Color(new A.RgbColorModelHex() { Val = "4BACC6" }),
              new A.Accent6Color(new A.RgbColorModelHex() { Val = "F79646" }),
              new A.Hyperlink(new A.RgbColorModelHex() { Val = "0000FF" }),
              new A.FollowedHyperlinkColor(new A.RgbColorModelHex() { Val = "800080" }))
            { Name = "Office" },
              new A.FontScheme(
              new A.MajorFont(
              new A.LatinFont() { Typeface = "Calibri" },
              new A.EastAsianFont() { Typeface = "" },
              new A.ComplexScriptFont() { Typeface = "" }),
              new A.MinorFont(
              new A.LatinFont() { Typeface = "Calibri" },
              new A.EastAsianFont() { Typeface = "" },
              new A.ComplexScriptFont() { Typeface = "" }))
              { Name = "Office" },
              new A.FormatScheme(
              new A.FillStyleList(
              new A.SolidFill(new A.SchemeColor() { Val = A.SchemeColorValues.PhColor }),
              new A.GradientFill(
                new A.GradientStopList(
                new A.GradientStop(new A.SchemeColor(new A.Tint() { Val = 50000 },
                  new A.SaturationModulation() { Val = 300000 })
                { Val = A.SchemeColorValues.PhColor })
                { Position = 0 },
                new A.GradientStop(new A.SchemeColor(new A.Tint() { Val = 37000 },
                 new A.SaturationModulation() { Val = 300000 })
                { Val = A.SchemeColorValues.PhColor })
                { Position = 35000 },
                new A.GradientStop(new A.SchemeColor(new A.Tint() { Val = 15000 },
                 new A.SaturationModulation() { Val = 350000 })
                { Val = A.SchemeColorValues.PhColor })
                { Position = 100000 }
                ),
                new A.LinearGradientFill() { Angle = 16200000, Scaled = true }),
              new A.NoFill(),
              new A.PatternFill(),
              new A.GroupFill()),
              new A.LineStyleList(
              new A.Outline(
                new A.SolidFill(
                new A.SchemeColor(
                  new A.Shade() { Val = 95000 },
                  new A.SaturationModulation() { Val = 105000 })
                { Val = A.SchemeColorValues.PhColor }),
                new A.PresetDash() { Val = A.PresetLineDashValues.Solid })
              {
                  Width = 9525,
                  CapType = A.LineCapValues.Flat,
                  CompoundLineType = A.CompoundLineValues.Single,
                  Alignment = A.PenAlignmentValues.Center
              },
              new A.Outline(
                new A.SolidFill(
                new A.SchemeColor(
                  new A.Shade() { Val = 95000 },
                  new A.SaturationModulation() { Val = 105000 })
                { Val = A.SchemeColorValues.PhColor }),
                new A.PresetDash() { Val = A.PresetLineDashValues.Solid })
              {
                  Width = 9525,
                  CapType = A.LineCapValues.Flat,
                  CompoundLineType = A.CompoundLineValues.Single,
                  Alignment = A.PenAlignmentValues.Center
              },
              new A.Outline(
                new A.SolidFill(
                new A.SchemeColor(
                  new A.Shade() { Val = 95000 },
                  new A.SaturationModulation() { Val = 105000 })
                { Val = A.SchemeColorValues.PhColor }),
                new A.PresetDash() { Val = A.PresetLineDashValues.Solid })
              {
                  Width = 9525,
                  CapType = A.LineCapValues.Flat,
                  CompoundLineType = A.CompoundLineValues.Single,
                  Alignment = A.PenAlignmentValues.Center
              }),
              new A.EffectStyleList(
              new A.EffectStyle(
                new A.EffectList(
                new A.OuterShadow(
                  new A.RgbColorModelHex(
                  new A.Alpha() { Val = 38000 })
                  { Val = "000000" })
                { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false })),
              new A.EffectStyle(
                new A.EffectList(
                new A.OuterShadow(
                  new A.RgbColorModelHex(
                  new A.Alpha() { Val = 38000 })
                  { Val = "000000" })
                { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false })),
              new A.EffectStyle(
                new A.EffectList(
                new A.OuterShadow(
                  new A.RgbColorModelHex(
                  new A.Alpha() { Val = 38000 })
                  { Val = "000000" })
                { BlurRadius = 40000L, Distance = 20000L, Direction = 5400000, RotateWithShape = false }))),
              new A.BackgroundFillStyleList(
              new A.SolidFill(new A.SchemeColor() { Val = A.SchemeColorValues.PhColor }),
              new A.GradientFill(
                new A.GradientStopList(
                new A.GradientStop(
                  new A.SchemeColor(new A.Tint() { Val = 50000 },
                    new A.SaturationModulation() { Val = 300000 })
                  { Val = A.SchemeColorValues.PhColor })
                { Position = 0 },
                new A.GradientStop(
                  new A.SchemeColor(new A.Tint() { Val = 50000 },
                    new A.SaturationModulation() { Val = 300000 })
                  { Val = A.SchemeColorValues.PhColor })
                { Position = 0 },
                new A.GradientStop(
                  new A.SchemeColor(new A.Tint() { Val = 50000 },
                    new A.SaturationModulation() { Val = 300000 })
                  { Val = A.SchemeColorValues.PhColor })
                { Position = 0 }),
                new A.LinearGradientFill() { Angle = 16200000, Scaled = true }),
              new A.GradientFill(
                new A.GradientStopList(
                new A.GradientStop(
                  new A.SchemeColor(new A.Tint() { Val = 50000 },
                    new A.SaturationModulation() { Val = 300000 })
                  { Val = A.SchemeColorValues.PhColor })
                { Position = 0 },
                new A.GradientStop(
                  new A.SchemeColor(new A.Tint() { Val = 50000 },
                    new A.SaturationModulation() { Val = 300000 })
                  { Val = A.SchemeColorValues.PhColor })
                { Position = 0 }),
                new A.LinearGradientFill() { Angle = 16200000, Scaled = true })))
              { Name = "Office" });

            theme1.Append(themeElements1);
            theme1.Append(new A.ObjectDefaults());
            theme1.Append(new A.ExtraColorSchemeList());

            themePart1.Theme = theme1;
            return themePart1;

        }
        #endregion
    }
}
