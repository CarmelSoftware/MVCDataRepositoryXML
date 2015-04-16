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
