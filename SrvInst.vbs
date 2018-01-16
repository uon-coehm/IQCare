' VBScript File

  ''shell("c:\iqcareservice\installiqcareservice.cmd")  
  Set objShell = CreateObject("WScript.Shell") 
  objShell.Run("c:\iqcareservice\installiqcareservice.cmd")
  objShell.Run("c:\iqcareservice\admin\IQSrvCtrl.exe") 



