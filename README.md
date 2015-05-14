# DND
Dungeons &amp; Dragons Maps!

<h1> How to make this shit work! </h1>
<ol>
<li> Clone project to C:/Git Projects/ </li>
<li> Grab the bak file in C:\Git Projects\DND\DnDMaps\Database Seed. Restore this as a new db in SQL Server 2012.</li>
<li> Once you've made your db, crack open the solution's web.config and change the DND_MAPS_Entities connection string to point to your db.</li>
<li> You may have some reference hell - I use a lot of Nuget stuff with this. If you expand the references folder in the "Maps" project (in vs), you should get little yellow warnings on any missing dlls. For any of these, remove the reference, and re-add it - the required dll should be within the "Libraries" directory of this repo.  </li>
<li> Ideally, this runs as .Net 4.5 in IIS 7.5/8 </li>
 
</ol>




