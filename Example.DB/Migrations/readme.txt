1.
Enable-Migrations -ProjectName Example.DB -StartUpProjectName Example.WebApi -Force

2.
Add-Migration "NewScript1" -ProjectName Example.DB -StartUpProjectName Example.WebApi

3.
Update-Database -ProjectName Example.DB -StartUpProjectName Example.WebApi

