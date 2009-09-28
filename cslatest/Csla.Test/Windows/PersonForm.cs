using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Csla.Test.Windows
{
  public partial class PersonForm : Form
  {
    public PersonForm()
    {
      InitializeComponent();
    }

    public void BindUI(EditablePerson person)
    {
      editablePersonBindingSource.DataSource = person;
      readWriteAuthorization1.ResetControlAuthorization();
    }

    private void editablePersonBindingSource_CurrentItemChanged(object sender, EventArgs e)
    {
      Debug.Print("applying authorization");
      readWriteAuthorization1.ResetControlAuthorization();
    }
  }
}
