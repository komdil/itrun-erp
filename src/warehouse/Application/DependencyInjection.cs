//using Application.Abstractions.Repositories;
//using Application.Abstractions.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
  public static class DependencyInjection
  {
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
      services.AddAutoMapper(typeof(DependencyInjection).Assembly);
      return services;
    }
  }
}