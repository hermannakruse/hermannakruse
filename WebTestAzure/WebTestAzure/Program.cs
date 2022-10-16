
using Microsoft.AspNetCore.HttpLogging;

public class Program
{

    public static HashSet<string> showSec = new HashSet<string>();
    public static DateTime nowD = DateTime.Now;
    public const string _target = "target=\"_blank\"";

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            logging.RequestHeaders.Add("sec-ch-ua");
            logging.ResponseHeaders.Add("MyResponseHeader");
            logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 1024 * 100;   // 4096;
            logging.ResponseBodyLogLimit = 1024 * 100;  //4096;
        });


        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        app.UseHttpLogging();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
