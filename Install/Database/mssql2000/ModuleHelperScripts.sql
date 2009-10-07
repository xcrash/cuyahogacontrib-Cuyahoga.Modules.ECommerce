create procedure GetModuleServices  
  
@moduletypeid int  
  
as  
  
select 'INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)  
VALUES (@moduletypeid, ''' + servicekey + ''', ''' + servicetype + ''', ''' + classtype + ''')'  
from cuyahoga_moduleservice where moduletypeid = @moduletypeid  

go
  
create procedure GetModuleSettings  
  
@moduletypeid int  
  
as  
  
select 'INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, '''   
+ name + ''', ''' + friendlyname + ''', ''' + settingdatatype + ''', ' + convert(varchar(1), iscustomtype) + ', ' + convert(varchar(1), isrequired) + ')'  
from cuyahoga_modulesetting where moduletypeid = @moduletypeid  

go