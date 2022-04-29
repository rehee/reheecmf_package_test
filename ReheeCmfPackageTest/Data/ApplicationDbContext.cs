using Entities;
using FormInputs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReheeCmf.DBContext;
using ReheeCmfPackageTest.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReheeCmfPackageTest.Data
{
  public class ApplicationDbContext : ReheeCMFDBContext<MyUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    [PublicEntity]
    public DbSet<Entity1> Entity1s { get; set; }
    [PublicEntity]
    public DbSet<EntityInput> EntityInputs { get; set; }
  }
  public class MyUser : IdentityUser, ICmfUser
  {

    public string ThisRoles
    {
      get
      {
        //var service = PropertyInject.GetService<UserManager<MyUser>>();
        //if (service != null)
        //{
        //  return String.Join(",", ReheeCmfTask.AwaiterResult(() => service.GetRolesAsync(this)));
        //}
        //return "";
        return thisRole;
      }
      set
      {
        this.thisRole = value;
      }
    }

    private string thisRole { get; set; }
    [FormInputs(InputType = EnumInputType.File)]
    public string Avatar { get; set; }
    [NotMapped]
    [FormInputs(InputType = EnumInputType.Text)]
    [Display(Name = "Test label")]
    public string AA { get; set; }

  }
}