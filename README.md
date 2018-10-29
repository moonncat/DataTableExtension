Welcome use DataTableExtension!!

With DataTableExtension, you can easily convert a DataTable into any Entity Model.

e.g.
using Dino.DataTableExtension;

//you can simply finish converting with just one line like below:
 var entitys = dt.ToEntity<EntityModel>();
 
 
 //or you can do it in this way:
var entitys = dt.ToEntity(typeof(EntityModel));
