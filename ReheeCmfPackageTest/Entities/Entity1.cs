using Entities;
using FormInputs;
using Grids;
using ReheeCmf.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ReheeCmfPackageTest.Entities
{
  [DataContract]
  [Grid(Icon = "fas fa-edit", OrderBy = "Id desc")]
  public class Entity1 : EntityBase<long>, IIdLong, ISelect
  {
    public Entity1()
    {
      
    }


    [DataMember]
    [GridColumn]
    public long Id2 { get; set; }
    [DataMember]
    [GridColumn]
    public long Id3
    {
      get
      {
        return Id2 + 1;
      }
      set
      {
        Id2 = value;
      }
    }

    
    [NotMapped]
    [IgnoreUpdate]
    [FormInputs(InputType = EnumInputType.Ignore)]
    public string SelectValue
    {
      get
      {
        return Id.StringValue();
      }
      set
      {

      }
    }
    [NotMapped]
    [IgnoreUpdate]
    [FormInputs(InputType = EnumInputType.Ignore)]
    public string SelectKey
    {
      get
      {
        return Id.StringValue();
      }
      set
      {

      }
    }
    [FormInputs(InputType = EnumInputType.Text)]
    public string StringKey3 { get; set; }
    [FormInputs(InputType = EnumInputType.Text)]
    public string StringKey4 { get; set; }
    [NotMapped]
    [IgnoreUpdate]
    [FormInputs(InputType = EnumInputType.Ignore)]
    public bool SelectDisplay
    {
      get
      {
        return true;
      }
      set
      {

      }
    }
  }
}
