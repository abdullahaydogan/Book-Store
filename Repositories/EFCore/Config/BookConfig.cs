using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.EFCore.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            
                builder.HasData(
                    new Book
                    {
                        Id = 1,
                        Price = 75,
                        Title = "Karagöz"
                    },
                     new Book
                     {
                         Id = 2,
                         Price = 75,
                         Title = "Mesnevi"
                     },
                      new Book
                      {
                          Id = 3,
                          Price = 75,
                          Title = "Devlet"
                      }


                    );
            
        }
    }
}
