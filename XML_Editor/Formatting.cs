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
public static string format(string s)
{
string F = "";
int n = 0;
int i = 0;
if (s[1] == '!' && s[1] == '?')
{
for (; i < s.Length; i++)
if (s[i] != '>')
F += s[i];
else
{
F += '>';
F += '\n';
break;
}
}
for (; i < s.Length; i++)
{
if (s[i] == ' ' || s[i] == '\n' || s[i] == '\r')
continue;
if (s[i + 1] == '/')
n--;
for (int j = 0; j < n; j++)
{
F += '\t';
}
if (s[i] == '<')
{
if (s[i + 1] != '/')
{
for (; i < s.Length; i++)
{
if (s[i] != '>')
F += s[i];
else
{
F += '>';
F += '\n';
break;
}
}
n++;
}
else
{
for (; i < s.Length; i++)
{
if (s[i] != '>')
F += s[i];
else
{
F += '>';
F += '\n';
break;
}
}



}
}
else
{
for (; i < s.Length; i++)
{
if (s[i] == '\n' || s[i] == '\r' || s[i] == '\t')
continue;
if (s[i] != '<')
F += s[i];
else
{
F += '\n';
i--;
break;
}
}
}



}
return F;
}

    }
}
