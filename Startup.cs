using ExamApplication.Data;
using ExamApplication.Models;
using ExamApplication.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ExamApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ExamAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ExamAppConString")));
            services.AddScoped<IQuestion,QuestionRepository>();
            services.AddScoped<IExam, ExamRepository>();
            services.AddScoped<IExamResult, ExamResultRepository>();
            services.AddScoped<IStudent, StudentRepository>();
            services.AddScoped<IInstructor, InstructorRepository>();
            services.AddScoped<DbContext,ExamAppDbContext>();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                           .AddEntityFrameworkStores<ExamAppDbContext>()
                           .AddDefaultTokenProviders();

            services.AddMvc();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseExceptionHandler("/Error");
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "instructorEditProfile",
                    pattern: "Instructor/EditProfile/{instructorId}",
                    defaults: new { controller = "Instructor", action = "EditProfile" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "studentExamPaper",
                    pattern: "{controller=Student}/{action=ExamPaper}/{id?}");


            });
        }
    }
}
