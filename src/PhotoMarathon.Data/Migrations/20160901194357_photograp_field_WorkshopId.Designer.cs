using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PhotoMarathon.Data;

namespace PhotoMarathon.Data.Migrations
{
    [DbContext(typeof(BaseDbContext))]
    [Migration("20160901194357_photograp_field_WorkshopId")]
    partial class photograp_field_WorkshopId
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PhotoMarathon.Data.Entities.Newsletter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Newsletters");
                });

            modelBuilder.Entity("PhotoMarathon.Data.Entities.Photographer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<bool>("HasNewsLetter");

                    b.Property<bool>("IsProfessionist");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PhoneNumber")
                        .IsRequired();

                    b.Property<bool>("RegisterForMarathon");

                    b.Property<bool>("RegisterForWorkShop");

                    b.Property<bool>("Rules");

                    b.Property<int?>("WorkshopId");

                    b.HasKey("Id");

                    b.HasIndex("WorkshopId");

                    b.ToTable("Photographers");
                });

            modelBuilder.Entity("PhotoMarathon.Data.Entities.WorkShop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("WorkShops");
                });

            modelBuilder.Entity("PhotoMarathon.Data.Entities.Photographer", b =>
                {
                    b.HasOne("PhotoMarathon.Data.Entities.WorkShop", "Workshop")
                        .WithMany("Photographers")
                        .HasForeignKey("WorkshopId");
                });
        }
    }
}
