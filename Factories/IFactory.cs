using league.Models;
using System.Collections.Generic;
namespace league.Factory
{
    public interface IFactory<T> where T : BaseEntity
    {
    }
}