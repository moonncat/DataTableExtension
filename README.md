Welcome use DataTableExtension!!

With DataTableExtension, you can easily convert a DataTable into any Entity Model.

e.g.
using Dino.DataTableExtension;

//you can simply finish converting with just one line like below:
 var entitys = dt.ToEntity&lt;EntityModel&gt;();
 
 
 //or you can do it in this way:
var entitys = dt.ToEntity(typeof(EntityModel));


if you want to do some group job, you can use GroupBy like below:
dt.GroupBy("colnamnName");
