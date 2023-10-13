using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rocosa_Modelos
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}
