using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception(AspectMessages.WrongValidationType);
            }

            _validatorType = validatorType;
        }

        protected override void OnBefore(IInvocation invocation)
        {
            //reflection: calisma aninda bir seyleri newlemeye yariyor. 
            //Validator icin bir instance uret.
            var validator = (IValidator)Activator.CreateInstance(_validatorType);
            //validator icin base type'a bak, onun da generic argumanindan ilkini al,
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];
            //validator type'a denk gelen metodun uygun tipine göre parametre olarak gecilen entity'i bul.
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
