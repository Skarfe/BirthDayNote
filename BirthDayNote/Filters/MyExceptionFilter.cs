using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BirthdayNote.Filters
{
    //Фильтр для ошибок
    public class MyExceptionFilter : ExceptionFilterAttribute
    {   public override void OnException(ExceptionContext context)
        {
            //Перенаправить на страницу с выводом сообщения что что-о пошло не так
            var result = new ViewResult { ViewName = "CustomError" };
            context.Result = result;
        }
    }
}
