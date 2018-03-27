using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using static MeatMarketAPI_s.Attributes;

namespace MeatMarketAPI_s
{
    public class Attributes
    {
        public static string _UserRegister;

        public string UserRegister { get { return _UserRegister; } set { _UserRegister = ""; } }

        public class CustomFilter : ActionFilterAttribute
        {
            public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
            {
               


                if (_UserRegister == "true")
                {
                    Console.WriteLine("Enter the user into website");
                }

                else
                {
                    Console.WriteLine("Access Denied");
                }
                base.OnActionExecuted(actionExecutedContext);
            }

            public override void OnActionExecuting(HttpActionContext actionContext)
            {
                //common logic 
                base.OnActionExecuting(actionContext);
            }

        }


        [AttributeUsage(AttributeTargets.Class,AllowMultiple = false, Inherited = true)]
        public class ZipCodeAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {// property

                if ((value as string).Length > 9) {
                    return new ValidationResult("zipCode must be less than 9 digits ");
                }



                //class
            var x= value.GetType().GetProperties();
                foreach (var item in x) {
                    if (((string)item.GetValue(value)).Length > 9) {
                        return new ValidationResult("zipCode must be less than 9 digits ");
                    } 
                }
                return ValidationResult.Success;
            }
        }
    }

    [ZipCode]
    public class Sql {
       [Required]
        [StringLength(maximumLength:2000)]
        public string ZipCode { get; set; }

        public void PrintCourseNAme() {

        }
    }

    public class test {
        public void Add(object a, object b) {
            Trainig<Sql> tr = new Trainig<Sql>();
            tr.Training();

            SqlTraining<Sql> sqlRe = new SqlTraining<Sql>();
        }
    }

    public class Trainig<T> where T:class {
        public virtual void Training( string course ="Manoj", string message=null) {
            //sql 
            Sql sql = new Sql();
            sql.PrintCourseNAme();

            // print course Name

            var type = typeof(T);
         var z=   type.GetCustomAttributes(typeof(CustomFilter), false);
          var t=  typeof(T).GetType().GetMethod("PrintCourseNAme");
          var x=  t.Invoke(Activator.CreateInstance(typeof(T)), new object[] { });
            

            if (typeof(T).Name == "Sql")
            {
            }


            Console.Write("Training going on" + course);
        }
    }

    public class SqlTraining<t>: Trainig<t> where t:class {
        public override void Training(string course, string message)
        {
            base.Training(course, message);
        }
    }
}