using Authenticates;
using Cruds;
using Entities;
using FormInputs;
using Grids;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReheeCmfPackageTest.Entities
{
  [Grid]
  public class EntityInput : EntityBase<long>, IIdLong
  {
    [FormInputs(InputType = EnumInputType.Switch)]
    public string Switch { get; set; }
    [FormInputs(InputType = EnumInputType.Text, Col = 6)]
    public string Text { get; set; }
    [FormInputs(InputType = EnumInputType.Text, Col = 3, InputMask = "(999)999999")]
    public string Tel { get; set; }
    [FormInputs(InputType = EnumInputType.Email, Col = 3)]
    public string Email { get; set; }
    [FormInputs(InputType = EnumInputType.Text)]
    public string Url { get; set; }
    [FormInputs(InputType = EnumInputType.Number, Max = "300")]
    public int Number { get; set; }
    [FormInputs(InputType = EnumInputType.Password)]
    public string Password { get; set; }
    [FormInputs(InputType = EnumInputType.TextArea)]
    public string TextArea { get; set; }
    [FormInputs(InputType = EnumInputType.Rich)]
    public string Rich { get; set; }
    [FormInputs(InputType = EnumInputType.Range, Max = "200")]
    public string Range { get; set; } = "44";
    [FormInputs(InputType = EnumInputType.DateTime)]
    public string Date { get; set; }
    [FormInputs(InputType = EnumInputType.Text)]
    public string Month { get; set; }
    [FormInputs(InputType = EnumInputType.Text)]
    public string Week { get; set; }
    [FormInputs(InputType = EnumInputType.DateTime)]
    public string DateTime { get; set; }
    [FormInputs(InputType = EnumInputType.Ignore)]
    public string File { get; set; }

    [FormInputs(InputType = EnumInputType.File, Max = "10")]
    [NotMapped]
    public IEnumerable<string> Files
    {
      get
      {
        return String.IsNullOrWhiteSpace(FileStrings) ? Enumerable.Empty<string>() : FileStrings.ToIEnumerable();
      }
      set
      {
        if (value == null)
        {
          this.FileStrings = null;
          return;
        }
        this.FileStrings = value.BackToString();
      }
    }
    [FormInputs(InputType = EnumInputType.Ignore)]
    public string FileStrings { get; set; }

    [NotMapped]
    [FormInputs(InputType = EnumInputType.Select)]
    public EnumType1 DropDown1
    {
      get
      {
        var result = DropDown.GetValue<EnumType1>();
        return result.Success ? result.Content : EnumType1.A;
      }
      set
      {
        DropDown = value.GetStringValue().Content;
      }
    }

    [FormInputs(InputType = EnumInputType.Ignore)]
    public string DropDown { get; set; }

    [NotMapped]
    [FormInputs(InputType = EnumInputType.Select)]
    public IEnumerable<EnumType1> DropDown2
    {
      get
      {
        var result = Radio.GetValue<IEnumerable<EnumType1>>();
        return result.Success ? result.Content : Enumerable.Empty<EnumType1>();
      }
      set
      {
        Radio = value.GetStringValue().Content;
      }
    }

    [FormInputs(InputType = EnumInputType.Ignore)]
    public string Radio { get; set; }

    [FormInputs(InputType = EnumInputType.Select, RelatedEntity = typeof(Entity1))]
    public string Select { get; set; }

    [NotMapped]
    [FormInputs(InputType = EnumInputType.Select, RelatedEntity = typeof(Entity1))]
    public string[] Select2
    {
      get
      {
        var result = CheckBox.GetValue<string[]>();
        return result.Success ? result.Content : new string[] { };
      }
      set
      {
        CheckBox = value.StringValue();
      }
    }

    [FormInputs(InputType = EnumInputType.Ignore)]
    public string CheckBox { get; set; }

    public override void AfterCreate(IContext context, TokenDTO user)
    {
      
    }
  }

  public enum EnumType1
  {
    A,
    B,
    C
  }
}
