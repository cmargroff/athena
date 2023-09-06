using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public  class StartManager: BaseMonoBehavior
{
    private ApplicationManager _applicationManager;
    private void Start()
    {
        _applicationManager = SafeFindObjectOfType<ApplicationManager>();
        _applicationManager.StartApplication();
    }
}
