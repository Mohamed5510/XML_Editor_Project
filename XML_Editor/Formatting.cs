using System;
using System.Collections.Generic;
using System.Text;

namespace XML_Editor
{
    /* Class Description:
     * 
     * 
     * 
     * 
     */
    internal class Formatting
    {
     
public static bool checkConsistency(string stringFile)
{
string[] xml = stringFile.Split("\n");
Stack<String> tag = new Stack<string>();
for (int i = 0; i < xml.Length; i++)
{
String line = xml[i];
for (int k = 0; k < line.Length; k++)
{
if (line[k] == '<')
{
if (line[k + 1] == '/')
{
if (!(tag.Count == 0))
{
string ppp = "";
int t = k + 1;
while (line[t++] != '>')
{
ppp += line[t];
}
//string ppp = line.Substring(k+2, tag.Peek().Length);
if (ppp != (tag.Peek()))
{
// unmatched closing and opening tags
return false;
}
else
{
tag.Pop();
}
}
else
{
// closing tag without opening tag
return false;
}
}
else if (line[k + 1] != '!' && line[k + 1] != '?')
{
String temp = "";
for (int j = k + 1; j < line.Length; j++)
{
if (line[j] != '>')
{
temp += line[j];
}
else
{
temp += '>';
break;
}
}
tag.Push(temp);
}
}
}
}
return ((tag.Count == 0));
} /* function input:
* function output:
* function description:
*
*
*
*/
public static string correct(string stringFile)
{
if (checkConsistency(stringFile))
return stringFile; string corrected = "";
string[] xml = stringFile.Split("\n");
Stack<String> tag = new Stack<string>();
for (int i = 0; i < xml.Length; i++)
{
String line = xml[i];
for (int k = 0; k < line.Length; k++)
{
corrected += line[k];
if (line[k] == '<')
{
if (line[k + 1] == '/')
{
if (!(tag.Count == 0))
{
string ppp = line.Substring(k);
if (ppp != ("</" + tag.Peek()))
{
// unmatched closing and opening tags
corrected = corrected + "/" + tag.Peek();
tag.Pop();
break;
}
else
{
tag.Pop();
}
}
else
{
// closing tag without opening tag
if (corrected[corrected.Length - 1] == '<')
corrected = corrected.Remove(corrected.Length - 1);
break;
}
}
else if (line[k + 1] != '!' && line[k + 1] != '?')
{
int e = corrected.Length - 1;
if (e > 2)
{
while (e-- > 0)
{
if (corrected[e] == '\n' || corrected[e] == ' ' || corrected[e] == '\r')
continue;
else if (corrected[e] == '>')
break;
else if (!(tag.Count == 0))
{
corrected = corrected + "/" + tag.Peek() + "\n";
corrected += '<';
tag.Pop();
break;
}
else
break;
}
}
String temp = "";
for (int j = k + 1; j < line.Length; j++)
{
if (line[j] != '>')
{
temp += line[j];
}
else
{
temp += '>';
break;
}
}
tag.Push(temp);
}
}
}
corrected += "\n";
}
while (!(tag.Count == 0))
{
corrected = corrected + "</" + tag.Peek() + "\n";
tag.Pop();
}
return (corrected);
}


    }
}
