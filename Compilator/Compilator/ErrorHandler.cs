using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilator
{
    public class ErrorHandler
    {
        private List<CompilerError> errors;

        public ErrorHandler()
        {
            errors = new List<CompilerError>();
        }

        public void AddError(CompilerError error)
        {
            errors.Add(error);
        }

        public void PrintErrors()
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }

        public bool HasErrors()
        {
            return errors.Any();
        }
    }

}
