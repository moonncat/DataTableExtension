Welcome use DataTableExtension!!

With DataTableExtension, you can easily convert a DataTable into any Entity Model.

e.g.
using Dino.DataTableExtension;

//you can simply finish converting with just one line like below:
<br/>
List&lt;EntityModel&gt; = dt.ToEntity&lt;EntityModel&gt;();
 
 
//or you can do it in this way:
<br/>
List&lt;EntityModel&gt; entitys = dt.ToEntity(typeof(EntityModel));


//Grouping by a column, you can use GroupBy like below:
<br/>
DataTable dt=sourceTable.GroupBy("colnamnName");


//Joining two tables together, you can use Join like below:
<br/>
DataTable dt=leftTable.Join(rightTable, "colnamnName");


//Sumary a column values, you can use Sum like below:
<br/>
int sum=dt.Sum&lt;int&gt; ("colnamnName");


//Exclude some row from a DataTable:
<br/>
int dt=sourceTable.Exculde((DataTable)table,"colnamnName");