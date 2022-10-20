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
            app.MapGet("/api/reports/sample",() =>
            {
                string filename = Path.Combine(app.Environment.WebRootPath, "lorem-ipsum.pdf");


                Stream stream = File.OpenRead(filename);

                return Results.File(stream, MediaTypeNames.Application.Pdf);

            });

            // GET api/reports?from=100&to=2000

            app.MapGet("/api/reports", (decimal? from, decimal? to, IDocumentService documentService) =>
            {
                // string filename = Path.Combine(app.Environment.WebRootPath, "lorem-ipsum.pdf");

                Stream stream = new MemoryStream();

                documentService.GenerateSamplePdf(stream);                

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
