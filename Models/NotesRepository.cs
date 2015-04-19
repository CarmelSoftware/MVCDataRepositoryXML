using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace IoCDependencyInjection.Models
{
public class NotesRepository : INotesRepository
{
private List<Note> notes = new List<Note>();
private int iNumberOfEntries = 1;
private XDocument doc;

public NotesRepository()
{
doc = XDocument.Load(HttpContext.Current.Server.MapPath("~/App_Data/data.xml"));
foreach (var node in doc.Descendants("note"))
{
notes.Add(new Note
{
ID = Int32.Parse(node.Descendants("id").FirstOrDefault().Value),
To = node.Descendants("to").FirstOrDefault().Value,
From = node.Descendants("from").FirstOrDefault().Value,
Heading = node.Descendants("heading").FirstOrDefault().Value,
Body = node.Descendants("body").FirstOrDefault().Value
});
}

iNumberOfEntries = notes.Count;
}

public IEnumerable<Note> GetAll()
{
return notes;
}
