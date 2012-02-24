using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Palaso.Code;
using Palaso.Progress.LogBox;

namespace LiftTools.Tools
{
	public class DuplicatedIdentifiers : Tool
	{
		#region Overrides of Tool

		public override void Run(string inputLiftPath, string outputLiftPath, IProgress progress)
		{
			RequireThat.File(inputLiftPath).Exists();
			Guard.AgainstNull(progress, "progress");

			var currentPrimarySet = new HashSet<string>();
			var currentSecondarySet = new HashSet<string>();
			var liftDoc = XDocument.Load(inputLiftPath);

			// Check out header element with its <ranges> and <fields> child elements.
			var headerElement = liftDoc.Root.Element("header");
			if (headerElement != null)
			{
				progress.WriteMessage("Checking the lift file <header>....");
				// Check <range> elements in <ranges> element.
				var currentHeaderChildElement = headerElement.Element("ranges");
				if (currentHeaderChildElement != null)
				{
					progress.WriteMessage("Checking the lift file <ranges>....");
					currentPrimarySet.Clear();
					foreach (var headerRangeElement in currentHeaderChildElement.Elements("range"))
					{
						// Check main <range> id attr.
						var rangeIdAttrValue = headerRangeElement.Attribute("id").Value;
						CheckDuplicateValue(progress, currentPrimarySet,
							string.Format("\tDuplicate <range> 'id' attribute found: '{0}'.", rangeIdAttrValue),
							rangeIdAttrValue);

						CheckCollectionOfMultiTextProperties(progress, new[] { "description", "label", "abbrev" },
							headerRangeElement,
							string.Format("\t\t<range> '{0}'", rangeIdAttrValue));

						// Check each <range-element> 'id' attr for dups within parent <range>, and dups within its multi-form properties.
						currentSecondarySet.Clear();
						foreach (var rangeElementElement in headerRangeElement.Elements("range-element"))
						{
							var rangeElementIdAttrValue = rangeElementElement.Attribute("id").Value;
							CheckDuplicateValue(progress, currentSecondarySet,
								string.Format("\t\t<range> '{0}' contains duplicate <range-element> 'id' attribute: '{1}'.", rangeIdAttrValue, rangeElementIdAttrValue),
								rangeElementIdAttrValue);

							CheckCollectionOfMultiTextProperties(progress, new[] {"description", "label", "abbrev"},
								rangeElementElement,
								string.Format("\t\t\t<range-element> '{0}' in <range> '{1}'", rangeElementIdAttrValue, rangeIdAttrValue));
						}
					}
				}

				currentHeaderChildElement = headerElement.Element("fields");
				if (currentHeaderChildElement != null)
				{
					progress.WriteMessage("Checking the lift file <fields>....");
					currentPrimarySet.Clear();
					foreach (var headerFieldElement in currentHeaderChildElement.Elements("field"))
					{
						var fieldTagAttrValue = headerFieldElement.Attribute("tag").Value;
						CheckDuplicateValue(progress, currentPrimarySet,
							string.Format("\tDuplicate <field> 'tag' attribute found: '{0}'.", fieldTagAttrValue),
							fieldTagAttrValue);

						CheckGaggleOfMultiTextStuff(progress, headerFieldElement,
							string.Format("\t\t<field> with 'tag' attribute '{0}' contains", fieldTagAttrValue));
					}
				}
			}
			
			currentPrimarySet.Clear();
			currentSecondarySet.Clear();
			progress.WriteMessage("Checking the lift file entries....");
			foreach (var entryElement in liftDoc.Root.Elements("entry"))
			{
				try
				{
					var entryid = entryElement.Attribute("id").Value;
					CheckDuplicateValue(progress, currentPrimarySet,
						string.Format("Duplicate <entry> 'id' attribute found: '{0}'.", entryid), entryid);
					var entryGuidAttr = entryElement.Attribute("guid");
					if (entryGuidAttr != null)
					{
						var entryGuid = entryGuidAttr.Value;
						CheckDuplicateValue(progress, currentSecondarySet,
							string.Format("Duplicate <entry> 'guid' attribute found: '{0}'.", entryGuid), entryGuid);
					}

					if (entryElement.Attribute("dateDeleted") != null)
						continue; // Nothing else to check on an entry.

					var baseEntryErrorMessage = string.Format("<entry> '{0}'", entryid);
					CheckCollectionOfMultiTextProperties(progress, new[] { "lexical-unit", "citation" },
						entryElement, baseEntryErrorMessage);

					// Other entry level elements:
					// Inherited from extensible
					CheckGaggleOfExtensibleStuff(progress, entryElement, baseEntryErrorMessage);
					// Defined on entry
					// pronunciation - zeroOrMore
					CheckPronunciations(progress, entryElement, baseEntryErrorMessage);
					// variant - zeroOrMore
					CheckVariants(progress, entryElement, baseEntryErrorMessage);
					// note - zeroOrMore
					CheckNotes(progress, entryElement, baseEntryErrorMessage);
					// relation - zeroOrMore
					CheckRelations(progress, entryElement, baseEntryErrorMessage);
					// etymology - zeroOrMore
					CheckEtymologies(progress, entryElement, baseEntryErrorMessage);
					// sense - zeroOrMore
					CheckSenses(progress, entryElement, "sense", baseEntryErrorMessage);
				}
				catch (Exception err)
				{
					throw err;
				}
			}
			progress.WriteMessage("********** Done **********");
		}

		private static void CheckSenses(IProgress progress, XContainer parentElement, string propertyName, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var senseSet = new HashSet<string>();
			foreach (var senseElement in parentElement.Elements(propertyName))
			{
				var senseid = senseElement.Attribute("id").Value;
				CheckDuplicateValue(progress, senseSet,
					string.Format("{0} contains duplicate <{1}> 'id' attribute '{2}'.", errorMessagebase, propertyName, senseid), senseid);
				var baseSenseErrorMessage = string.Format("{0}" + Environment.NewLine + "  with <{1}> with 'id' of '{2}'", errorMessagebase, propertyName, senseid);
				CheckGaggleOfExtensibleStuff(progress, senseElement, baseSenseErrorMessage);
				CheckGrammaticalInfo(progress, senseElement, baseSenseErrorMessage);
				CheckBasicMultiTextStuff(progress, senseElement, "gloss", baseSenseErrorMessage);
				CheckCollectionOfMultiTextProperties(progress, new[] { "definition" }, senseElement, baseSenseErrorMessage);
				CheckRelations(progress, senseElement, baseSenseErrorMessage);
				CheckNotes(progress, senseElement, baseSenseErrorMessage);
				// example - zeroOrMore
				CheckExamples(progress, senseElement, baseSenseErrorMessage);
				// reversal - zeroOrMore
				CheckReversals(progress, senseElement, baseSenseErrorMessage);
				// illustration - zeroOrMore (renamed URLRef instance)
				var illustrationSet = new HashSet<string>();
				var illustrationBaseErrorMessage = string.Format(Environment.NewLine + "    {0} <illustration> property contains", baseSenseErrorMessage);
				foreach (var mediaElement in senseElement.Elements("illustration"))
				{
					CheckUrlRef(progress, illustrationSet, mediaElement, illustrationBaseErrorMessage);
				}
				// subsense - zeroOrMore - infinite recursion of senses.
				CheckSenses(progress, senseElement, "subsense", baseSenseErrorMessage);
			}
		}

		private static void CheckReversals(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			//var reversalSet = new HashSet<string>();
			foreach (var reversalElement in parentElement.Elements("reversal"))
			{
				var reversalErrorMessageBase = string.Format("{0} has a <reversal>", errorMessagebase);
				//// Optional 'type' attr, but unique w/in translation
				//var typeAttr = reversalElement.Attribute("type");
				//if (typeAttr != null)
				//{
				//    var typeValue = typeAttr.Value;
				//    CheckDuplicateValue(progress, reversalSet, string.Format("{0} with a duplicate 'type' attribute '{1}'", reversalErrorMessageBase, typeValue), typeValue);
				//}
				CheckGaggleOfMultiTextStuff(progress, reversalElement, reversalErrorMessageBase);
				CheckReversals(progress, reversalElement.Element("main"), reversalErrorMessageBase); // Nested reversal, but under a new name.
				CheckGrammaticalInfo(progress, reversalElement, reversalErrorMessageBase);
			}
		}

		private static void CheckExamples(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			foreach (var exampleElement in parentElement.Elements("example"))
			{
				var exampleBaseErrorMessage = string.Format("{0} with unknown <example>", errorMessagebase);
				CheckGaggleOfMultiTextStuff(progress, exampleElement, exampleBaseErrorMessage);
				CheckGaggleOfExtensibleStuff(progress, exampleElement, exampleBaseErrorMessage);
				CheckTranslations(progress, exampleElement, exampleBaseErrorMessage);
				CheckNotes(progress, exampleElement, exampleBaseErrorMessage);
			}
		}

		private static void CheckTranslations(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var transSet = new HashSet<string>();
			foreach (var translationElement in parentElement.Elements("translation"))
			{
				var transErrorMessageBase = string.Format("{0} has a <translation>", errorMessagebase);
				// Optional 'type' attr, but unique w/in translation
				var typeAttr = translationElement.Attribute("type");
				if (typeAttr != null)
				{
					var typeValue = typeAttr.Value;
					CheckDuplicateValue(progress, transSet, string.Format("{0} with a duplicate 'type' attribute '{1}'", transErrorMessageBase, typeValue), typeValue);
				}
				CheckGaggleOfMultiTextStuff(progress, translationElement, transErrorMessageBase);
			}
		}

		private static void CheckGrammaticalInfo(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;
			var grammaticalInfoElement = parentElement.Element("grammatical-info");
			if (grammaticalInfoElement == null)
				return;

			var giValueValue = grammaticalInfoElement.Attribute("value").Value;
			// Check traits
			CheckTraits(progress,
				grammaticalInfoElement,
				string.Format("{0} contains <grammatical-info> with 'value' attribute '{1}' and contains", errorMessagebase, giValueValue));
		}

		private static void CheckEtymologies(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var etymologySet = new HashSet<string>();
			foreach (var etymologyElement in parentElement.Elements("etymology"))
			{
				var typeValue = etymologyElement.Attribute("type").Value;
				var sourceValue = etymologyElement.Attribute("source").Value;
				var etymologyCombinedKey = typeValue + "_" + sourceValue;
				var etymologyErrorMessageBase = string.Format("\t{0} <relation> with duplicate combined key of '{1}' and '{2}'", errorMessagebase, typeValue, sourceValue);
				CheckDuplicateValue(progress, etymologySet, etymologyErrorMessageBase, etymologyCombinedKey);
				CheckGaggleOfExtensibleStuff(progress, etymologyElement, etymologyErrorMessageBase);
				// multiple bare <form> elements
				CheckBasicMultiTextStuff(progress, etymologyElement, "form", errorMessagebase);
				// multiple bare <gloss> elements
				CheckBasicMultiTextStuff(progress, etymologyElement, "gloss", errorMessagebase);
			}
		}

		private static void CheckNotes(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var noteSet = new HashSet<string>();
			foreach (var noteElement in parentElement.Elements("note"))
			{
				// 'type' is optional.
				var noteErrorMessageBase = string.Format("{0} <note> of unknown 'type' contains", errorMessagebase);
				var typeAttr = noteElement.Attribute("type");
				if (typeAttr != null)
				{
					var typeValue = noteElement.Attribute("type").Value;
					noteErrorMessageBase = string.Format("{0} <note> of 'type' '{1}' contains", errorMessagebase, typeValue);
					CheckDuplicateValue(progress, noteSet,
						string.Format("\t{0} <note> with duplicate 'type' attribute '{1}'", errorMessagebase, typeValue), typeValue);
				}

				CheckGaggleOfMultiTextStuff(progress, noteElement, noteErrorMessageBase);
				CheckGaggleOfExtensibleStuff(progress, noteElement, noteErrorMessageBase);
			}
		}

		private static void CheckVariants(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			foreach (var variantElement in parentElement.Elements("variant"))
			{
				// TODO: How to do this? SteveMc suggests some fancy multiple <form> 'lang' attr + string contents key

				var variantErrorMessageBase = string.Format("{0} contains unidentifed <variant> that contains", errorMessagebase);
				CheckGaggleOfMultiTextStuff(progress, variantElement, variantErrorMessageBase);
				CheckGaggleOfExtensibleStuff(progress, variantElement, variantErrorMessageBase);

				CheckPronunciations(progress, variantElement, variantErrorMessageBase);
				CheckRelations(progress, variantElement, variantErrorMessageBase);
			}
		}

		private static void CheckRelations(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			//var relationSet = new HashSet<string>();
			foreach (var relationElement in parentElement.Elements("relation"))
			{
				var relationErrorMessageBase = string.Format("\t{0} <relation> property has", errorMessagebase);
				//var typeValue = relationElement.Attribute("type").Value;
				//var refValue = relationElement.Attribute("ref").Value;
				//var relationCombinedKey = typeValue + "_" + refValue;
				//var relationErrorMessageBase = string.Format("\t{0} <relation> property has duplicate combined key of '{1}' and '{2}'", errorMessagebase, typeValue, refValue);
				//CheckDuplicateValue(progress, relationSet, relationErrorMessageBase, relationCombinedKey);
				CheckGaggleOfExtensibleStuff(progress, relationElement, relationErrorMessageBase);
				CheckCollectionOfMultiTextProperties(progress, new[] { "usage" }, relationElement, relationErrorMessageBase);
			}
		}

		private static void CheckCollectionOfMultiTextProperties(IProgress progress, IEnumerable<string> elementNames, XContainer parentElement, string errorMessage)
		{
			if (parentElement == null)
				return;

			foreach (var elementName in elementNames)
			{
				CheckGaggleOfMultiTextStuff(progress, parentElement.Element(elementName),
											String.Format("{0} <{1}> property contains", errorMessage, elementName));
			}
		}

		private static void CheckGaggleOfMultiTextStuff(IProgress progress, XContainer parentElement, string errorMessage)
		{
			if (parentElement == null)
				return;

			CheckBasicMultiTextStuff(progress, parentElement, "form", errorMessage);
		}

		private static void CheckBasicMultiTextStuff(IProgress progress, XContainer parentElement, string propertyName, string errorMessage)
		{
			if (parentElement == null)
				return;

			var langSet = new HashSet<string>();
			foreach (var langAttrContaingElement in parentElement.Elements(propertyName))
			{
				var langAttrValue = langAttrContaingElement.Attribute("lang").Value;
				CheckDuplicateValue(progress, langSet, string.Format("{0} duplicate <{1}> 'lang' attribute '{2}'.", errorMessage, propertyName, langAttrValue), langAttrValue);
				// a <fpropertyNameorm> element can contain zeroOrMore <annotation> elements.
				CheckAnnotations(progress, langAttrContaingElement,
					string.Format("{0} <{1}> with 'lang' '{2}' contains", errorMessage, propertyName, langAttrValue));
			}
		}

		private static void CheckGaggleOfExtensibleStuff(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			CheckGaggleOfExtensibleStuffSansFields(progress, parentElement, errorMessagebase);

			CheckFields(progress, parentElement, string.Format("{0} contains a", errorMessagebase));
		}

		private static void CheckGaggleOfExtensibleStuffSansFields(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			CheckAnnotations(progress, parentElement, string.Format("{0} contains an", errorMessagebase)); // annotation - zeroOrMore
			CheckTraits(progress, parentElement, string.Format("{0} contains a", errorMessagebase)); // trait - zeroOrMore
		}

		private static void CheckFields(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var fieldCombinedKeySet = new HashSet<string>();
			foreach (var fieldElement in parentElement.Elements("field"))
			{
				var typeValue = fieldElement.Attribute("type").Value;
				CheckDuplicateValue(progress, fieldCombinedKeySet,
					string.Format("\t{0} <field> with duplicate 'type' attribute '{1}'", errorMessagebase, typeValue), typeValue);
				// <field> has zeroOrMore <form> elements.
				var baseErrorMessage = string.Format("\t{0} <field> with 'type' attribute '{1}' contains", errorMessagebase, typeValue);
				CheckGaggleOfMultiTextStuff(progress, fieldElement, baseErrorMessage);
				// <field> contains the gaggle of extensible stuff.
				CheckGaggleOfExtensibleStuff(progress, fieldElement, baseErrorMessage);
			}
		}

		private static void CheckTraits(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var traitCombinedKeySet = new HashSet<string>();
			foreach (var traitElement in parentElement.Elements("trait"))
			{
				var nameValue = traitElement.Attribute("name").Value;
				var valueValue = traitElement.Attribute("value").Value;
				var combinedKey = nameValue + "_" + valueValue;
				CheckDuplicateValue(progress, traitCombinedKeySet,
					string.Format("\t{0} <trait> with duplicate combined key of '{1}' and '{2}'", errorMessagebase, nameValue, valueValue), combinedKey);
				// <trait> has zeroOrMore <form> elements.
				CheckGaggleOfMultiTextStuff(progress, traitElement,
					string.Format("\t{0} <trait> with 'name' '{1}' and 'value' '{2}' contains", errorMessagebase, nameValue, valueValue));
			}
		}

		private static void CheckPronunciations(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			foreach (var pronunciationElement in parentElement.Elements("pronunciation"))
			{
				// TODO: How to do this? SteveMc suggests some fancy multiple <form> 'lang' attr + string contents key

				var pronunciationErrorMessageBase = string.Format("{0} contains unidentifed pronunciation that contains", errorMessagebase);
				CheckGaggleOfMultiTextStuff(progress, pronunciationElement, pronunciationErrorMessageBase);
				CheckGaggleOfExtensibleStuff(progress, pronunciationElement, pronunciationErrorMessageBase);
				var mediaUrlSet = new HashSet<string>();
				var mediaBaseErrorMessage = string.Format("{0} <media> property contains", errorMessagebase);
				foreach (var mediaElement in pronunciationElement.Elements("media"))
				{
					CheckUrlRef(progress, mediaUrlSet, mediaElement, mediaBaseErrorMessage);
				}
			}
		}

		private static void CheckUrlRef(IProgress progress, HashSet<string> urlSet, XElement parentElement, string baseErrorMessage)
		{
			if (parentElement == null)
				return;
			var hrefValue = parentElement.Attribute("href").Value;
			CheckDuplicateValue(progress, urlSet, string.Format("{0} contains dulicate URL '{1}'.", baseErrorMessage, hrefValue), hrefValue);
			// Check <label>.
			CheckCollectionOfMultiTextProperties(progress, new[] { "label" }, parentElement, baseErrorMessage);
		}

		private static void CheckAnnotations(IProgress progress, XContainer parentElement, string errorMessagebase)
		{
			if (parentElement == null)
				return;

			var annotationCombinedKeySet = new HashSet<string>();
			foreach (var annotationElement in parentElement.Elements("annotation"))
			{
				var nameValue = annotationElement.Attribute("name").Value;
				var valueValue = annotationElement.Attribute("value").Value;
				var combinedKey = nameValue + "_" + valueValue;
				CheckDuplicateValue(progress, annotationCombinedKeySet,
					string.Format("\t{0} <annotation> with duplicate combined key of '{1}' and '{2}'", errorMessagebase, nameValue, valueValue), combinedKey);
				// <annotation> has zeroOrMore <form> elements.
				CheckGaggleOfMultiTextStuff(progress, annotationElement,
					string.Format("\t{0} <annotation> with 'name' '{1}' and 'value' '{2}' contains", errorMessagebase, nameValue, valueValue));
			}
		}

		private static void CheckDuplicateValue(IProgress progress, HashSet<string> currentSet, string errorMessage, string keyAttrValue)
		{
			if (currentSet.Contains(keyAttrValue))
			{
				progress.WriteMessageWithColor("red", errorMessage);
			}
			else
			{
				currentSet.Add(keyAttrValue);
			}
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