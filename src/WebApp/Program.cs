using eAgenda.Aplicacao.Compartilhado;
var builder = WebApplication.CreateBuilder(args);

//Configura camada de Infraestrutura

//Configura camada de Aplicação
builder.Services.AdicionarCamadaAplicacao();
//Configura camada de Apresentação
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
       {
           // Reseta a configuração padrão do MVC
           options.ViewLocationFormats.Clear();

           // Localização das Views dos módulos: Modulos/ModuloAluno/Views/Listar.cshtml
           options.ViewLocationFormats.Add("/Modulos/Modulo{1}/Views/{0}.cshtml");

           // Localização das Views compartilhadas: /Compartilhado/Views/_Layout.cshtml
           options.ViewLocationFormats.Add("/Compartilhado/Views/{0}.cshtml");
       });

var app = builder.Build();

// Acesso aos arquivos CSS e JS dentro da wwwroot
app.UseStaticFiles();

app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
