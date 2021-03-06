---
title: "ArgData API: PitCrewColorWriter Class"
---

[API Reference](/argdata/api/) &gt; [0.19](/argdata/api/0.19/) &gt; PitCrewColorWriter

# PitCrewColorWriter Class

Writes pit crew colors of one or more teams.

**Namespace:** ArgData

## Constructors

<table class="table table-bordered table-striped ">
<thead>
  <tr>
    <th>Name</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>PitCrewColorWriter(GpExeFile <em>exeFile</em>)</td>
    <td>Creates a PitCrewColorWriter for the specified GP.EXE file.<br /><em>exeFile</em>: GpExeFile to read from.<br /></td>
  </tr>
</tbody>
</table>


## Methods

<table class="table table-bordered table-striped ">
<thead>
  <tr>
    <th>Name</th>
    <th>Description</th>
  </tr>
</thead>
<tbody>
  <tr>
    <td>For(GpExeFile <em>exeFile</em>)</td>
    <td>Creates a PitCrewColorWriter for the specified GP.EXE file.<br /><em>exeFile</em>: GpExeFile to read from.<br /></td>
  </tr>
  <tr>
    <td>WritePitCrewColors(PitCrew <em>pitCrew</em>, Int32 <em>teamIndex</em>)</td>
    <td>Writes pit crew colors for a team.<br /><em>pitCrew</em>: PitCrew with colors to write.<br /><em>teamIndex</em>: Index of the team to write the colors for.<br /></td>
  </tr>
  <tr>
    <td>WritePitCrewColors(PitCrewList <em>pitCrewList</em>)</td>
    <td>Writes pit crew colors for all the teams.<br /><em>pitCrewList</em>: PitCrewList with colors to write.<br /></td>
  </tr>
</tbody>
</table>


