using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne(x => x.Price) //Na coluna price, ele tem currency e value (isso está definido na linha 15)
                .Property(x => x.Currency);

            builder.OwnsOne(x => x.Price)
                .Property(x => x.Value);
        }
    }
}
