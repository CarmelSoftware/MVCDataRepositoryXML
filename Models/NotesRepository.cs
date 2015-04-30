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
public Note Get(int id)
{
return notes.Find(p => p.ID == id);
}
public Note Add(Note item)
{
if (item == null)
{
throw new ArgumentNullException("item");
}

item.ID = iNumberOfEntries++;

XElement newNode = new XElement("note");
XElement id = new XElement("id"); id.Value = item.ID.ToString() ;
XElement to = new XElement("to");to.Value = item.To;
XElement from = new XElement("from"); from.Value = item.From;
XElement heading = new XElement("heading"); heading.Value = item.Heading;
XElement body = new XElement("body"); body.Value = item.Body;
newNode.Add(id, to, from, heading, body);
doc.Root.Add(newNode);
SaveXML();
return item;
}
public bool Update(Note item)
{
if (item == null)
{
throw new ArgumentNullException("item");
} 
XElement note = doc.Descendants("note").Where(n => Int32.Parse( n.Descendants("id").FirstOrDefault().Value ) == item.ID ).FirstOrDefault();
note.Descendants("to").FirstOrDefault().Value = item.To;
note.Descendants("from").FirstOrDefault().Value = item.From;
note.Descendants("heading").FirstOrDefault().Value = item.Heading;
note.Descendants("body").FirstOrDefault().Value = item.Body;
SaveXML();
return true;
}
public bool Delete(int id)
{
doc.Root.Descendants("note").Where(n => Int32.Parse(n.Descendants("id").First().Value ) == id).Remove() ;
SaveXML();
return true;
}

private void SaveXML()
{
doc.Save(HttpContext.Current.Server.MapPath("~/App_Data/data.xml"));
}
}
}
