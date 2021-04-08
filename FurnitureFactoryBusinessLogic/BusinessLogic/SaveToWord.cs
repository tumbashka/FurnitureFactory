using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using FurnitureFactoryBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FurnitureFactoryBusinessLogic.BusinessLogic
{
    public class SaveToWord
    {
        public static void CreateDoc(WordInfo info)
        {
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(info.FileName, WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body docBody = mainPart.Document.AppendChild(new Body());
                docBody.AppendChild(CreateParagraph(new WordParagraph
                {
                    Texts = new List<string> { info.Title },
                    TextProperties = new WordParagraphProperties
                    {
                        Bold = true,
                        Size = "24",
                        JustificationValues = JustificationValues.Center
                    }
                }));
                Table table = new Table();
                TableProperties tblProp = new TableProperties(
                    new TableBorders(
                        new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 },
                        new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 8 }
                    )
                );
                table.AppendChild<TableProperties>(tblProp);
                TableRow headerRow = new TableRow();
                TableCell headerNumberCell = new TableCell(new Paragraph(new Run(new Text("№"))));
                TableCell headerTypeCell = new TableCell(new Paragraph(new Run(new Text("Тип"))));
                TableCell headerNameCell = new TableCell(new Paragraph(new Run(new Text("Название модели"))));
                TableCell headerDimensionsCell = new TableCell(new Paragraph(new Run(new Text("Габариты"))));
                TableCell headerPriceCell = new TableCell(new Paragraph(new Run(new Text("Цена"))));
                TableCell headerCountCell = new TableCell(new Paragraph(new Run(new Text("Кол-во"))));
                headerRow.Append(headerNumberCell);
                headerRow.Append(headerTypeCell);
                headerRow.Append(headerNameCell);
                headerRow.Append(headerDimensionsCell);
                headerRow.Append(headerPriceCell);
                headerRow.Append(headerCountCell);
                table.Append(headerRow);
                int i = 1;
                int sum = 0;
                foreach (var model in info.Models)
                {
                    sum += model.Key * model.Value.Price;
                    TableRow currentRow = new TableRow();
                    TableCell numberCell = new TableCell(new Paragraph(new Run(new Text(i.ToString()))));
                    TableCell typeCell = new TableCell(new Paragraph(new Run(new Text(model.Value.TypeName))));
                    TableCell nameCell = new TableCell(new Paragraph(new Run(new Text(model.Value.ModelName))));
                    TableCell dimensionsCell = new TableCell(new Paragraph(new Run(new Text(model.Value.Dimensions))));
                    TableCell priceCell = new TableCell(new Paragraph(new Run(new Text(model.Value.Price.ToString()))));
                    TableCell countCell = new TableCell(new Paragraph(new Run(new Text(model.Key.ToString()))));
                    currentRow.Append(numberCell);
                    currentRow.Append(typeCell);
                    currentRow.Append(nameCell);
                    currentRow.Append(dimensionsCell);
                    currentRow.Append(priceCell);
                    currentRow.Append(countCell);
                    table.Append(currentRow);
                    i++;
                }
                TableRow lastRow = new TableRow();
                TableCell emptyCell1 = new TableCell(new Paragraph(new Run(new Text(" "))));
                TableCell emptyCell2 = new TableCell(new Paragraph(new Run(new Text(" "))));
                TableCell emptyCell3 = new TableCell(new Paragraph(new Run(new Text(" "))));
                TableCell emptyCell4 = new TableCell(new Paragraph(new Run(new Text(" "))));
                lastRow.Append(emptyCell1);
                lastRow.Append(emptyCell2);
                lastRow.Append(emptyCell3);
                TableCell itogoCell = new TableCell(new Paragraph(new Run(new Text("Итого:"))));
                TableCell sumCell = new TableCell(new Paragraph(new Run(new Text(sum.ToString()))));
                lastRow.Append(itogoCell);
                lastRow.Append(sumCell);
                lastRow.Append(emptyCell4);
                table.Append(lastRow);

                docBody.Append(table);
                docBody.AppendChild(CreateSectionProperties());
                wordDocument.MainDocumentPart.Document.Save();
            }
        }
        private static SectionProperties CreateSectionProperties()
        {
            SectionProperties properties = new SectionProperties();
            PageSize pageSize = new PageSize
            {
                Orient = PageOrientationValues.Portrait
            };
            properties.AppendChild(pageSize);
            return properties;
        }
        private static Paragraph CreateParagraph(WordParagraph paragraph)
        {
            if (paragraph != null)
            {
                Paragraph docParagraph = new Paragraph();
                docParagraph.AppendChild(CreateParagraphProperties(paragraph.TextProperties));
                foreach (var run in paragraph.Texts)
                {
                    Run docRun = new Run();
                    RunProperties properties = new RunProperties();
                    properties.AppendChild(new FontSize
                    {
                        Val = paragraph.TextProperties.Size
                    });
                    if (paragraph.TextProperties.Bold)
                    {
                        properties.AppendChild(new Bold());
                    }
                    docRun.AppendChild(properties);
                    docRun.AppendChild(new Text
                    {
                        Text = run,
                        Space = SpaceProcessingModeValues.Preserve
                    });
                    docParagraph.AppendChild(docRun);
                }
                return docParagraph;
            }
            return null;
        }
        private static ParagraphProperties
        CreateParagraphProperties(WordParagraphProperties paragraphProperties)
        {
            if (paragraphProperties != null)
            {
                ParagraphProperties properties = new ParagraphProperties();
                properties.AppendChild(new Justification()
                {
                    Val = paragraphProperties.JustificationValues
                });
                properties.AppendChild(new SpacingBetweenLines
                {
                    LineRule = LineSpacingRuleValues.Auto
                });
                properties.AppendChild(new Indentation());
                ParagraphMarkRunProperties paragraphMarkRunProperties = new ParagraphMarkRunProperties();
                if (!string.IsNullOrEmpty(paragraphProperties.Size))
                {
                    paragraphMarkRunProperties.AppendChild(new FontSize
                    {
                        Val = paragraphProperties.Size
                    });
                }
                if (paragraphProperties.Bold)
                {
                    paragraphMarkRunProperties.AppendChild(new Bold());
                }
                properties.AppendChild(paragraphMarkRunProperties);
                return properties;
            }
            return null;
        }
    }
}
