using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCRB.WEB.ModelBinder
{
    public class ModelBinderProvider : IModelBinderProvider
    {
        private readonly ILoggerFactory _logger;

        public ModelBinderProvider(ILoggerFactory logger)
        {
            _logger = logger;
        }

        [Obsolete]
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (!context.Metadata.IsComplexType && (context.Metadata.ModelType == typeof(DateTime?)) || context.Metadata.ModelType == typeof(DateTime)) // only encode string types
            {
                return new ModelBinderDateTime(new SimpleTypeModelBinder(context.Metadata.ModelType, _logger));
            }

            return null;
        }


    }
}