using eAgenda.Aplicacao.Compartilhado;
using eAgenda.Infraestrutura.Compartilhado;
using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

//Configura camada de Infraestrutura
builder.Services.AdicionarCamadaInfraestrutura(builder.Configuration);

//Configura camada de Aplicação
builder.Services.AdicionarCamadaAplicacao();

//Configura camada de Apresentação
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
       {
           // Reseta a configuração padrão do MVC
           options.ViewLocationFormats.Clear();

           // Localização das Views dos módulos: Modulos/Alunos/Apresentacao/Views/Listar.cshtml
           options.ViewLocationFormats.Add("/Modulos/{1}s/Views/{0}.cshtml");

           // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
           options.ViewLocationFormats.Add("/Compartilhado/Views/{0}.cshtml");
       });

var app = builder.Build();

//Aplica migrações pendentes do banco de dados (somente em ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<EAgendaDbContext>();

    if (dbContext.Database.IsSqlServer())
        dbContext.Database.Migrate();
}

// Acesso aos arquivos CSS e JS dentro da wwwroot
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
