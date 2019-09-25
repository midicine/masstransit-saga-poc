using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace POC.Saga.Infrastructure
{
    public class ConfirmInvitationMapping : IEntityTypeConfiguration<ConfirmInvitationState>
    {
        public void Configure(EntityTypeBuilder<ConfirmInvitationState> builder)
        {
            builder.ToTable("ConfirmInvitation", "saga");
            builder.HasKey(x => x.CorrelationId);
            builder.Property(x => x.RowVersion)
                .IsRowVersion();
        }
    }
}
