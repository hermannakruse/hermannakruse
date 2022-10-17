
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpLogging;
using System.Drawing;
using System.Net;
using System.Net.Mime;

public class Program
{

    public static HashSet<string> showSec = new HashSet<string>();
    public static DateTime nowD = DateTime.Now;
    public const string _target = "target=\"_blank\"";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //builder.Services.AddHttpLogging(logging =>
        //{
        //    logging.LoggingFields = HttpLoggingFields.All;
        //    logging.RequestHeaders.Add("sec-ch-ua");
        //    logging.ResponseHeaders.Add("MyResponseHeader");
        //    logging.MediaTypeOptions.AddText("application/javascript");
        //    logging.RequestBodyLogLimit = 1024 * 100;   // 4096;
        //    logging.ResponseBodyLogLimit = 1024 * 100;  //4096;
        //});


        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        //app.UseHttpLogging();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Use(async (context, next) =>
        {
            if (context.Request.Path.Value.StartsWith("/bilder/"))
            {
                HttpResponse response = context.Response;
                response.ContentType = "image/Jpeg";


                string source = System.IO.Path.Combine(app.Environment.WebRootPath, context.Request.Path.Value.Substring(1));

                var bmp = new Bitmap(source);

                Bitmap bw = bmp.Clone(new Rectangle(0, 0, bmp.Width, bmp.Height),System.Drawing.Imaging.PixelFormat.Format4bppIndexed);

        

                var mSt = new MemoryStream();
                bw.Save(mSt,System.Drawing.Imaging.ImageFormat.Jpeg);
                mSt.Position = 0;
                await mSt.CopyToAsync(response.Body);

                //using (var pic = System.IO.File.Open(source, FileMode.Open))
                //{
                //    await pic.CopyToAsync(response.Body);
                //}
            }
            else
            {
                await next.Invoke();
            }
            // Do logging or other work that doesn't write to the Response.
        });


        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
