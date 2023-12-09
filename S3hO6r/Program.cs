using System.Text.RegularExpressions;

namespace S3hO6r;
class Program
{
        //Purpose of application as originally stated:
        //In the programming language of your choice, write a program that parses a sentence and replaces each word with the following:
        //first letter, number of distinct characters between first and last character, and last letter. Words are separated by spaces
        //or non-alphabetic characters and these separators should be maintained in their original form and location in the answer.
        //
        //Examples:
        //
        //1 “Smooth” becomes “S3h”
        //2 “Space separated” becomes “S3e s5d”
        //3 “Dash-separated” becomes “D2h-s5d”
        //4 “Number2separated” becomes “N4r2s5d”

        // What I intend for this application to do:
        // This program will parse a sentence of undetermined length.  A word within the sentence is defined as a string of strictly alpha characters.
        // Separaters within the sentence are defined as any string of non-alpha characters.  This program will replace each word with the first alpha
        // character, then the number (using numeric characters) of distinct alpha characters between the first and last alpha character, then the last
        // alpha character. Seperators will be maintained and can be any non-alpha character.
        // Some assumptions:
        // I'm using Win11 and it has a reported command line length limit of 8191 characters so this will not support a sentence longer than that
        // I am somewhat unsure how to handle a "word" that is shorter than three characters so a word that is two characters will be modified to
        // the first alpha, a zero, and then the last alpha.  A word made of one character will not be modified.
        // For this application lowercase alpha is distinct from the same uppercase alpha, in other words a != A



    static void Main(string[] args)
    {
        const bool testMode = true;

        if (args.Length == 0)
        {
            if (testMode)
            {
                Tester();
            }
            else
            {
                Console.WriteLine("S3hO6r");
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine("This program will parse a sentence, replace each word with the first alpha,");
                Console.WriteLine("number of distinct alpha characters between first and last alpha, and last");
                Console.WriteLine("alpha. Seperators will be maintained and can be any non-alpha character.");
                Console.WriteLine("");
                Console.WriteLine("Example usage: S3hO6r example4me please");
                Console.WriteLine("");
                Console.WriteLine("Example output: e5e4m0e p4e");
            }
        }
        else
        {
            Console.WriteLine(Processor(String.Join(" ", args)));
        }
    }

    static string Processor(string input)
    {
        var output = "";
        if (input.Length > 0)
        {
            var alphaIsFirst = Char.IsLetter(input[0]);
            var sepMatcher = new Regex("[^a-zA-z]+");
            var wordMatcher = new Regex("[a-zA-z]+");
            var words = wordMatcher.Matches(input);
            var seperators = sepMatcher.Matches(input);

            for (int i = 0; i < words.Count || i < seperators.Count; i++)
            {
                if (alphaIsFirst && i < words.Count)
                {
                    output += ModifyWord(words[i].ToString());
                }
                if (i < seperators.Count)
                {
                    output += seperators[i];
                }
                if (!alphaIsFirst && i < words.Count)
                {
                    output += ModifyWord(words[i].ToString());
                }
            }
        }
        return output;
    }

    static string ModifyWord(string input)
    {
        var output = input;

        if (input.Length > 1)
        {
            var firstChar = input.First();
            var middle = input.Substring(1, input.Length - 2);
            var lastChar = input.Last();

            output = firstChar + middle.ToCharArray().Distinct().Count().ToString() + lastChar;
        }

        return output;
    }

    static void Tester()
    {
        Console.WriteLine(Processor(""));
        Console.WriteLine(Processor("a"));
        Console.WriteLine(Processor("1"));
        Console.WriteLine(Processor("Smooth"));
        Console.WriteLine(Processor("Space separated"));
        Console.WriteLine(Processor("Dash-separated"));
        Console.WriteLine(Processor("Number2separated"));
        Console.WriteLine(Processor("12345"));
        Console.WriteLine(Processor("a1ab2abc3"));
        Console.WriteLine(Processor("AAaa"));
        Console.WriteLine(Processor("     "));
        Console.WriteLine(Processor("     something     "));
        Console.WriteLine(Processor("ahhhhh!!!!"));
        Console.WriteLine(Processor("ASCII(/ ˈæskiː / ⓘ ASS - kee),[3]: 6 abbreviated from American Standard Code for Information Interchange, is a character encoding standard for electronic communication. ASCII codes represent text in computers, telecommunications equipment, and other devices.Because of technical limitations of computer systems at the time it was invented, ASCII has just 128 code points, of which only 95 are printable characters, which severely limited its scope.Modern computer systems have evolved to use Unicode, which has millions of code points, but the first 128 of these are the same as the ASCII set."));
        Console.WriteLine(Processor("	0	1	2	3	4	5	6	7	8	9	A	B	C	D	E	F0x  NUL SOH STX ETX EOT ENQ ACK BEL  BS      HT      LF      VT      FF      CR      SO      SI1x  DLE DC1 DC2 DC3 DC4 NAK SYN ETB CAN  EM     SUB ESC  FS      GS      RS      US2x   SP!   \"	#	$	%	&	'	(	)	*	+	,	-	.	/3x  0   1   2   3   4   5   6   7   8   9	:	;   <   =   >   ?4x  @	A B   C D   E F   G H   I J   K L   M N   O5x P   Q R   S T   U V   W X   Y Z[	\\	]   ^_6x  `	a b   c d   e f   g h   i j   k l   m n   o7x p   q r   s t   u v   w x   y z   {   |   }~DEL"));
    }
}