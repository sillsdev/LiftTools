using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Palaso.Code;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
	public class DuplicatedIdentifiers : Tool
	{
		private IProgress _progress;

		#region Overrides of Tool

		public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
		{
			RequireThat.File(inputLiftPath).Exists();
			Guard.AgainstNull(progress, "progress");

			_progress = progress;

			var currentSet = new HashSet<string>();
			var liftDoc = XDocument.Load(inputLiftPath);

			// Check out header element.
			// For now, only work with root/header/fields/field (<form> elements).
			foreach (var headerFieldElement in liftDoc.Root.Element("header").Element("fields").Elements("field"))
			{
				var fieldTagAttrValue = headerFieldElement.Attribute("tag").Value;
				currentSet.Clear();
				foreach (var headerFieldFormAttrValue in headerFieldElement.Elements("form").Select(formAltElement => formAltElement.Attribute("lang").Value))
				{
					if (currentSet.Contains(headerFieldFormAttrValue) || currentSet.Contains(headerFieldFormAttrValue.ToLowerInvariant()))
					{
						sb.AppendFormat("Found header field form element with duplicate 'lang' attribute '{0}' in header field with tag '{1}'", headerFieldFormAttrValue, fieldTagAttrValue);
					}
					else
					{
						currentSet.Add(headerFieldFormAttrValue);
					}
				}
			}

			foreach (var entryElement in liftDoc.Root.Elements("entry"))
			{
				if (entryElement.Attribute("dateDeleted") != null)
					continue;

				var entryGuid = entryElement.Attribute("guid").Value;

				// 1. Check out "form' alts in:
				/*
<lexical-unit>
	<form
		lang="en">
		<text>my lex unit form</text>
	</form>
</lexical-unit>
				*/
				currentSet.Clear();
				foreach (var formLang in entryElement.Element("lexical-unit").Elements("form").Select(formAltElement => formAltElement.Attribute("lang").Value))
				{
					if (currentSet.Contains(formLang) || currentSet.Contains(formLang.ToLowerInvariant()))
					{
						sb.AppendFormat("Found lexical-unit form element with duplicate 'lang' attribute '{0}' in entry with guid '{1}'", formLang, entryGuid);
					}
					else
					{
						currentSet.Add(formLang);
					}
				}

				// 2. Check out form alts in:
				/*
<citation>
	<form
		lang="en">
		<text>my citation form</text>
	</form>
</citation>
				*/
				currentSet.Clear();
				var citElement = entryElement.Element("citation");
				if (citElement != null)
				{
					foreach (var formLang in entryElement.Element("citation").Elements("form").Select(formAltElement => formAltElement.Attribute("lang").Value))
					{
						if (currentSet.Contains(formLang) || currentSet.Contains(formLang.ToLowerInvariant()))
						{
							sb.AppendFormat("Found citation form element with duplicate 'lang' attribute '{0}' in entry with guid '{1}'", formLang, entryGuid);
						}
						else
						{
							currentSet.Add(formLang);
						}
					}
				}

				// Check out dups in entry level:
				// type attr is a key, so assume multiple entry field elements
				// Assume repeating <form> elments in the <field> element.
				/*
<field type="scientific-name">
	<form lang="en"><text>fancy latin term</text></form>
</field>
				*/
				currentSet.Clear();
				foreach (var entryFieldElement in entryElement.Elements("field"))
				{
					var typeAttrValue = entryFieldElement.Attribute("type").Value;
					if (currentSet.Contains(typeAttrValue) || currentSet.Contains(typeAttrValue.ToLowerInvariant()))
					{
						sb.AppendFormat("Found field element with duplicate 'type' attribute '{0}' in entry with guid '{1}'", typeAttrValue, entryGuid);
					}
					else
					{
						currentSet.Add(typeAttrValue);
					}

					// Now check for dup lang attrs on form elements.
					var fieldFormSet = new HashSet<string>();
					foreach (var fieldFormAttrValue in entryFieldElement.Elements("form").Select(formAltElement => formAltElement.Attribute("lang").Value))
					{
						if (fieldFormSet.Contains(fieldFormAttrValue) || fieldFormSet.Contains(fieldFormAttrValue.ToLowerInvariant()))
						{
							sb.AppendFormat("Found field element with duplicate 'lang' attribute in field of type '{0}' with a form 'lang' of '{1}' in entry with guid '{2}'", typeAttrValue, fieldFormAttrValue, entryGuid);
						}
						else
						{
							fieldFormSet.Add(fieldFormAttrValue);
						}
					}
				}

				// Check out dup form lang attrs in label of illustration:
				// Assume:
				//	1. multiple <illustration> elements per entry,
				//	2. multiple <label> elements per <illustration> elemtn, and
				//	3. multiple <form> elements per <label> (Only testable keyed element.)
				/*
<illustration href="MyPic.jpg">
	<label>
		<form lang="es"><text>cucaracha</text></form>
		<form lang="en"><text>cockroach</text></form>
	</label>
</illustration>
				*/
				foreach (var illustrationElement in entryElement.Elements("illustration"))
				{
					foreach (var labelElement in illustrationElement.Elements("label"))
					{
						currentSet.Clear();
						foreach (var labelFormAttrValue in labelElement.Elements("form").Select(formAltElement => formAltElement.Attribute("lang").Value))
						{
							if (currentSet.Contains(labelFormAttrValue) || currentSet.Contains(labelFormAttrValue.ToLowerInvariant()))
							{
								sb.AppendFormat("Found field element with duplicate 'lang' attribute in some label of some illustration with a 'lang' attribute of '{0}' in entry with guid '{1}'", labelFormAttrValue, entryGuid);
							}
							else
							{
								currentSet.Add(labelFormAttrValue);
							}
						}
					}
				}

				// Check out duplicate sense ids (the sense id attr is what is used in the lift merge code for finding a matching sense.)
				// But a dup guid is just as bad, so report it, too. But then, a sense may not have a guid attr.
				currentSet.Clear();
				foreach (var senseElement in entryElement.Elements("sense"))
				{
					var senseId = senseElement.Attribute("id").Value;
					if (currentSet.Contains(senseId) || currentSet.Contains(senseId.ToLowerInvariant()))
					{
						sb.AppendFormat("Found sense element with duplicate id attribute '{0}' in entry with guid '{1}'", senseId, entryGuid);
					}
					else
					{
						currentSet.Add(senseId);
					}

					// Check out duplicate glosses.
					/*
<gloss
	lang="en">
	<text>hot</text>
</gloss>
<gloss
	lang="es">
	<text>caliente</text>
</gloss>
					*/
					var glossSet = new HashSet<string>();
					foreach (var glossLangAttrValue in senseElement.Elements("gloss").Select(glossElement => glossElement.Attribute("lang").Value))
					{
						if (glossSet.Contains(glossLangAttrValue) || glossSet.Contains(glossLangAttrValue.ToLowerInvariant()))
						{
							sb.AppendFormat("Found gloss element with duplicate lang attribute '{0}' in sense with id '{1}' in entry with guid '{2}'", glossLangAttrValue, senseId, entryGuid);
						}
						else
						{
							glossSet.Add(glossLangAttrValue);
						}
					}

					// Check out duplicate definition forms
					/*
<definition>
	<form
		lang="en">
		<text>hot</text>
	</form>
	<form
		lang="es">
		<text>caliente</text>
	</form>
</definition>
					*/
					var definitionFormsSet = new HashSet<string>();
					foreach (var definitionFormLangAttrValue in senseElement.Elements("definition").Elements("form").Select(glossElement => glossElement.Attribute("lang").Value))
					{
						if (definitionFormsSet.Contains(definitionFormLangAttrValue) || definitionFormsSet.Contains(definitionFormLangAttrValue.ToLowerInvariant()))
						{
							sb.AppendFormat("Found definition form element with duplicate lang attribute '{0}' in sense with id '{1}' in entry with guid '{2}'", definitionFormLangAttrValue, senseId, entryGuid);
						}
						else
						{
							definitionFormsSet.Add(definitionFormLangAttrValue);
						}
					}

					// Check out examples.
					// Assumptions:
					//	1. There can be muiltiple examples.
					//	2. Each example can have multiple forms.
					//	3. Each example can have multiple translation elements each of which can have multiple form elements.
					// The assumptions may not hold, but they may flush out more dups.
					/*
<example>
	<form lang="es"><text>Mis cosas.</text></form>
	<translation>
	<form lang="en"><text>My stuff.</text></form>
	</translation>
</example>
					*/
					foreach (var exampleElement in senseElement.Elements("example"))
					{
						var exampleFormsSet = new HashSet<string>();
						foreach (var exampleFormLangAttrValue in exampleElement.Elements("form").Select(exampleFormElement => exampleFormElement.Attribute("lang").Value))
						{
							if (exampleFormsSet.Contains(exampleFormLangAttrValue) || exampleFormsSet.Contains(exampleFormLangAttrValue.ToLowerInvariant()))
							{
								sb.AppendFormat("Found example form element with duplicate lang attribute '{0}' in some example in the sense with id '{1}' in entry with guid '{2}'", exampleFormLangAttrValue, senseId, entryGuid);
							}
							else
							{
								exampleFormsSet.Add(exampleFormLangAttrValue);
							}
						}
						foreach (var exampleTranslationElement in exampleElement.Elements("translation"))
						{
							var exampleTranslationFormsSet = new HashSet<string>();
							foreach (var exampleTranslationFormLangAttrValue in exampleTranslationElement.Elements("form").Select(exampleTranlationFormElement => exampleTranlationFormElement.Attribute("lang").Value))
							{
								if (exampleTranslationFormsSet.Contains(exampleTranslationFormLangAttrValue) || exampleTranslationFormsSet.Contains(exampleTranslationFormLangAttrValue.ToLowerInvariant()))
								{
									sb.AppendFormat("Found example translation form element with duplicate lang attribute '{0}' in some example's translation in the sense with id '{1}' in entry with guid '{2}'", exampleTranslationFormLangAttrValue, senseId, entryGuid);
								}
								else
								{
									exampleTranslationFormsSet.Add(exampleTranslationFormLangAttrValue);
								}
							}
						}
					}
				}
			}
			progress.WriteMessage("********** Done **********");
		}

		public override string InfoPageName
		{
			get { return "DuplicatedIdentifiers.htm"; }
		}

		#endregion

		public override string ToString()
		{
			return "Locate Duplicated Identifiers";
		}
	}
}