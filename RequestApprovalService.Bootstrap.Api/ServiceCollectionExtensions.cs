using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Newtonsoft.Json.Serialization;
using RequestApprovalService.Silverspoon.Wire.Abstractions;

namespace RequestApprovalService.Bootstrap.Api
{
    public static class ServiceCollectionExtensions
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public static IServiceCollection AddComponents(this IServiceCollection services, string configuration,
            string searchPattern)
        {
            //var currentDirectory = Directory.GetCurrentDirectory();
            //var reflectionPath = Assembly.GetExecutingAssembly().Location;
            //var index = reflectionPath.LastIndexOf("\\");
            //var path = reflectionPath.Substring(0, index);
            //var sourcePath = Path.Combine(currentDirectory, "bin");
            //var sourcePathFiles = Directory.GetFiles(sourcePath, "*.dll");


            var currentDirectory = Directory.GetCurrentDirectory();
            var sourcePath = Path.Combine(currentDirectory, "Include");
            var sourcePathFiles = Directory.GetFiles(sourcePath, "*.dll");

            //var certificate = sourcePathFiles.ToDictionary(file => file,
            //    file => X509Certificate.CreateFromSignedFile(file).GetCertHashString());


            List<Assembly> assemblies = new List<Assembly>();
            foreach (var assemblyFile in sourcePathFiles)
            {
                assemblies.Add(Assembly.LoadFrom(assemblyFile));
            }

            foreach (var assembly in assemblies)
            {
                services.AddMvc().AddApplicationPart(assembly).AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                }).AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(assembly);
                    fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

                services.AddMediatR(assembly);

                var wireType = typeof(IWire);
                var tmp = assembly.ExportedTypes.Where(x =>
                        wireType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(x =>
                    {
                        var next = Activator.CreateInstance(x);

                        return next;
                    }).Cast<IWire>().FirstOrDefault();
                if (tmp != null)
                    services = tmp.Couple(services, configuration);
            }

            //Load file based on pattern
            return services;
        }
    }
}
