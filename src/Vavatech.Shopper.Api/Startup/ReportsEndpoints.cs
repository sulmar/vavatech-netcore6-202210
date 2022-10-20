using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net.Mime;

namespace Vavatech.Shopper.Api.Startup
{
    public static class ReportsEndpoints
    {
        public static WebApplication MapReportsEndpoints(this WebApplication app)
        {
            // GET api/reports?from=100&to=2000

            app.MapGet("/api/reports", (decimal? from, decimal? to) =>
            {
                string filename = Path.Combine(app.Environment.WebRootPath, "lorem-ipsum.pdf");

                // TODO: generate report
                //  Stream stream = File.OpenRead(filename);

                Stream stream = new MemoryStream();

                // dotnet add package QuestPDF
                // dotnet add package QuestPDF.Previewer
                // https://www.questpdf.com/quick-start
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                            .Text("Hello PDF!")
                            .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                x.Item().Text(Placeholders.LoremIpsum());
                                x.Item().Image(Placeholders.Image(200, 100));
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x =>
                            {
                                x.Span("Page ");
                                x.CurrentPageNumber();
                            });
                    });
                })
                .GeneratePdf(stream);

                // stream.Position = 0;
                stream.Seek(0, SeekOrigin.Begin);

                return Results.File(stream, MediaTypeNames.Application.Pdf);

            });


            app.MapPost("/api/reports", async (HttpRequest request) =>
            {
                if (!request.HasFormContentType)
                    return Results.BadRequest();

                // var form = request.Form;
                var form = await request.ReadFormAsync();

                var file = form.Files["file1"];

                var customerId = int.Parse(form["customerId"]);

                if (file == null)
                    return Results.BadRequest();

                var path = Path.Combine("uploads", file.FileName);
                using var writeStream = File.OpenWrite(path);
                using var readStream = file.OpenReadStream();

                TextReader reader = new StreamReader(readStream);
                StreamWriter writer = File.AppendText(path);

                // TODO: nie zapisuje do pliku
                writer.WriteLine("Hello World!");

                //while (reader.Peek() >= 0)
                //{
                //    string? line = reader.ReadLine();
                //    writer.WriteLine(line + "!");
                //}




                await readStream.CopyToAsync(writeStream);


                // writeStream.Position = 0;
                //writeStream.Flush();
                //writeStream.Close();

                return Results.Accepted();

                // return Results.File(writeStream);

            });


            return app;
        }
    }
}
