using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Persistence.Extensions
{
    internal static class SoftDeleteModelBuilderExtensions
    {
        public static ModelBuilder ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
        {
            foreach (var mutableEntityType in modelBuilder.Model.GetEntityTypes())
            {

                // check if current entity type is child of BaseModel
                if (mutableEntityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
                {
                    var param = Expression.Parameter(mutableEntityType.ClrType, "entity");
                    var prop = Expression.PropertyOrField(param, nameof(BaseEntity.IsDeleted));

                    var value = Expression.Constant(false);
                    var converted = Expression.Convert(value, prop.Type);
                    var expression1 = Expression.Equal(prop, converted);

                    var value2 = Expression.Constant(null);
                    var converted2 = Expression.Convert(value2, prop.Type);
                    var expression2 = Expression.Equal(prop, converted2);

                    var body = Expression.Or(expression1, expression2);

                    var entityNotDeleted = Expression.Lambda(body, param);

                    mutableEntityType.SetQueryFilter(entityNotDeleted);
                }
            }

            return modelBuilder;
        }
    }
}
