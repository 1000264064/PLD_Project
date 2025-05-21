
using System;
using System.IO;
using System.Runtime.Serialization;
using com.calitha.goldparser.lalr;
using com.calitha.commons;
using System.Collections.Generic;

namespace com.calitha.goldparser
{

    [Serializable()]
    public class SymbolException : System.Exception
    {
        public SymbolException(string message) : base(message)
        {
        }

        public SymbolException(string message,
            Exception inner) : base(message, inner)
        {
        }

        protected SymbolException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

    }

    [Serializable()]
    public class RuleException : System.Exception
    {

        public RuleException(string message) : base(message)
        {
        }

        public RuleException(string message,
                             Exception inner) : base(message, inner)
        {
        }

        protected RuleException(SerializationInfo info,
                                StreamingContext context) : base(info, context)
        {
        }

    }

    enum SymbolConstants : int
    {
        SYMBOL_EOF             =  0, // (EOF)
        SYMBOL_ERROR           =  1, // (Error)
        SYMBOL_WHITESPACE      =  2, // Whitespace
        SYMBOL_MINUS           =  3, // '-'
        SYMBOL_PERCENT         =  4, // '%'
        SYMBOL_LPAREN          =  5, // '('
        SYMBOL_RPAREN          =  6, // ')'
        SYMBOL_TIMES           =  7, // '*'
        SYMBOL_DIV             =  8, // '/'
        SYMBOL_DIVDIV          =  9, // '//'
        SYMBOL_COLON           = 10, // ':'
        SYMBOL_PLUS            = 11, // '+'
        SYMBOL_LT              = 12, // '<'
        SYMBOL_LTEQ            = 13, // '<='
        SYMBOL_LTGT            = 14, // '<>'
        SYMBOL_EQ              = 15, // '='
        SYMBOL_EQEQ            = 16, // '=='
        SYMBOL_GT              = 17, // '>'
        SYMBOL_GTEQ            = 18, // '>='
        SYMBOL_ELIF            = 19, // elif
        SYMBOL_ELSE            = 20, // else
        SYMBOL_FOR             = 21, // for
        SYMBOL_IDENTIFIER      = 22, // Identifier
        SYMBOL_IF              = 23, // if
        SYMBOL_IN              = 24, // in
        SYMBOL_INTEGER         = 25, // Integer
        SYMBOL_RANGE           = 26, // range
        SYMBOL_STRINGLITERAL   = 27, // StringLiteral
        SYMBOL_WHILE           = 28, // while
        SYMBOL_ADDEXP          = 29, // <Add Exp>
        SYMBOL_ASSIGNMENT      = 30, // <Assignment>
        SYMBOL_ELSEIFSTATEMENT = 31, // <ElseIfStatement>
        SYMBOL_ELSESTATEMENT   = 32, // <ElseStatement>
        SYMBOL_EXPRESSION      = 33, // <Expression>
        SYMBOL_FORLOOP         = 34, // <ForLoop>
        SYMBOL_IFSTATEMENT     = 35, // <IfStatement>
        SYMBOL_LOOP            = 36, // <Loop>
        SYMBOL_MULTEXP         = 37, // <Mult Exp>
        SYMBOL_NEGATEEXP       = 38, // <Negate Exp>
        SYMBOL_PROGRAM         = 39, // <Program>
        SYMBOL_STATEMENT       = 40, // <Statement>
        SYMBOL_STATEMENTLIST   = 41, // <StatementList>
        SYMBOL_VALUE           = 42, // <Value>
        SYMBOL_WHILELOOP       = 43  // <WhileLoop>
    };

    enum RuleConstants : int
    {
        RULE_PROGRAM                                                     =  0, // <Program> ::= <StatementList>
        RULE_STATEMENTLIST                                               =  1, // <StatementList> ::= <Statement>
        RULE_STATEMENTLIST2                                              =  2, // <StatementList> ::= <Statement> <StatementList>
        RULE_STATEMENT                                                   =  3, // <Statement> ::= <Assignment>
        RULE_STATEMENT2                                                  =  4, // <Statement> ::= <IfStatement>
        RULE_STATEMENT3                                                  =  5, // <Statement> ::= <Loop>
        RULE_ASSIGNMENT_IDENTIFIER_EQ                                    =  6, // <Assignment> ::= Identifier '=' <Expression>
        RULE_IFSTATEMENT_IF_COLON                                        =  7, // <IfStatement> ::= if <Expression> ':' <StatementList> <ElseIfStatement>
        RULE_ELSEIFSTATEMENT_ELIF_COLON                                  =  8, // <ElseIfStatement> ::= elif <Expression> ':' <StatementList> <ElseIfStatement>
        RULE_ELSEIFSTATEMENT                                             =  9, // <ElseIfStatement> ::= <ElseStatement>
        RULE_ELSEIFSTATEMENT2                                            = 10, // <ElseIfStatement> ::= 
        RULE_ELSESTATEMENT_ELSE_COLON                                    = 11, // <ElseStatement> ::= else ':' <StatementList>
        RULE_LOOP                                                        = 12, // <Loop> ::= <ForLoop>
        RULE_LOOP2                                                       = 13, // <Loop> ::= <WhileLoop>
        RULE_FORLOOP_FOR_IDENTIFIER_IN_RANGE_LPAREN_INTEGER_RPAREN_COLON = 14, // <ForLoop> ::= for Identifier in range '(' Integer ')' ':' <StatementList>
        RULE_WHILELOOP_WHILE_COLON                                       = 15, // <WhileLoop> ::= while <Expression> ':' <StatementList>
        RULE_EXPRESSION                                                  = 16, // <Expression> ::= <Add Exp>
        RULE_EXPRESSION_GT                                               = 17, // <Expression> ::= <Add Exp> '>' <Expression>
        RULE_EXPRESSION_LT                                               = 18, // <Expression> ::= <Add Exp> '<' <Expression>
        RULE_EXPRESSION_LTEQ                                             = 19, // <Expression> ::= <Add Exp> '<=' <Expression>
        RULE_EXPRESSION_GTEQ                                             = 20, // <Expression> ::= <Add Exp> '>=' <Expression>
        RULE_EXPRESSION_EQEQ                                             = 21, // <Expression> ::= <Add Exp> '==' <Expression>
        RULE_EXPRESSION_LTGT                                             = 22, // <Expression> ::= <Add Exp> '<>' <Expression>
        RULE_ADDEXP                                                      = 23, // <Add Exp> ::= <Mult Exp>
        RULE_ADDEXP_PLUS                                                 = 24, // <Add Exp> ::= <Mult Exp> '+' <Add Exp>
        RULE_ADDEXP_MINUS                                                = 25, // <Add Exp> ::= <Mult Exp> '-' <Add Exp>
        RULE_MULTEXP                                                     = 26, // <Mult Exp> ::= <Negate Exp>
        RULE_MULTEXP_TIMES                                               = 27, // <Mult Exp> ::= <Negate Exp> '*' <Mult Exp>
        RULE_MULTEXP_DIV                                                 = 28, // <Mult Exp> ::= <Negate Exp> '/' <Mult Exp>
        RULE_MULTEXP_DIVDIV                                              = 29, // <Mult Exp> ::= <Negate Exp> '//' <Mult Exp>
        RULE_MULTEXP_PERCENT                                             = 30, // <Mult Exp> ::= <Negate Exp> '%' <Mult Exp>
        RULE_NEGATEEXP_MINUS                                             = 31, // <Negate Exp> ::= '-' <Value>
        RULE_NEGATEEXP                                                   = 32, // <Negate Exp> ::= <Value>
        RULE_VALUE_IDENTIFIER                                            = 33, // <Value> ::= Identifier
        RULE_VALUE_INTEGER                                               = 34, // <Value> ::= Integer
        RULE_VALUE_STRINGLITERAL                                         = 35, // <Value> ::= StringLiteral
        RULE_VALUE_LPAREN_RPAREN                                         = 36  // <Value> ::= '(' <Expression> ')'
    };

    public class MyParser
    {
        private LALRParser parser;
        ListBox lst1;
        ListBox lst2;
        public MyParser(string filename, ListBox lst1, ListBox lst2)
        {
            FileStream stream = new FileStream(filename,
                                               FileMode.Open, 
                                               FileAccess.Read, 
                                               FileShare.Read);
            this.lst1 = lst1;
            this.lst2 = lst2;
            Init(stream);
            stream.Close();
        }

        public MyParser(string baseName, string resourceName)
        {
            byte[] buffer = ResourceUtil.GetByteArrayResource(
                System.Reflection.Assembly.GetExecutingAssembly(),
                baseName,
                resourceName);
            MemoryStream stream = new MemoryStream(buffer);
            Init(stream);
            stream.Close();
        }

        public MyParser(Stream stream)
        {
            Init(stream);
        }

        private void Init(Stream stream)
        {
            CGTReader reader = new CGTReader(stream);
            parser = reader.CreateNewParser();
            parser.TrimReductions = false;
            parser.StoreTokens = LALRParser.StoreTokensMode.NoUserObject;

            parser.OnTokenError += new LALRParser.TokenErrorHandler(TokenErrorEvent);
            parser.OnParseError += new LALRParser.ParseErrorHandler(ParseErrorEvent);
            parser.OnTokenRead += new LALRParser.TokenReadHandler(TokenReadEvent);
        }

        public void Parse(string source)
        {
            NonterminalToken token = parser.Parse(source);
            if (token != null)
            {
                Object obj = CreateObject(token);
                //todo: Use your object any way you like
            }
        }

        private Object CreateObject(Token token)
        {
            if (token is TerminalToken)
                return CreateObjectFromTerminal((TerminalToken)token);
            else
                return CreateObjectFromNonterminal((NonterminalToken)token);
        }

        private Object CreateObjectFromTerminal(TerminalToken token)
        {
            switch (token.Symbol.Id)
            {
                case (int)SymbolConstants.SYMBOL_EOF :
                //(EOF)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ERROR :
                //(Error)
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHITESPACE :
                //Whitespace
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MINUS :
                //'-'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PERCENT :
                //'%'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LPAREN :
                //'('
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RPAREN :
                //')'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_TIMES :
                //'*'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIV :
                //'/'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_DIVDIV :
                //'//'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_COLON :
                //':'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PLUS :
                //'+'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LT :
                //'<'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTEQ :
                //'<='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LTGT :
                //'<>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQ :
                //'='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EQEQ :
                //'=='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GT :
                //'>'
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_GTEQ :
                //'>='
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELIF :
                //elif
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSE :
                //else
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FOR :
                //for
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IDENTIFIER :
                //Identifier
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IF :
                //if
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IN :
                //in
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_INTEGER :
                //Integer
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_RANGE :
                //range
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STRINGLITERAL :
                //StringLiteral
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILE :
                //while
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ADDEXP :
                //<Add Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ASSIGNMENT :
                //<Assignment>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSEIFSTATEMENT :
                //<ElseIfStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_ELSESTATEMENT :
                //<ElseStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_EXPRESSION :
                //<Expression>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_FORLOOP :
                //<ForLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_IFSTATEMENT :
                //<IfStatement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_LOOP :
                //<Loop>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_MULTEXP :
                //<Mult Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_NEGATEEXP :
                //<Negate Exp>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_PROGRAM :
                //<Program>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENT :
                //<Statement>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_STATEMENTLIST :
                //<StatementList>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_VALUE :
                //<Value>
                //todo: Create a new object that corresponds to the symbol
                return null;

                case (int)SymbolConstants.SYMBOL_WHILELOOP :
                //<WhileLoop>
                //todo: Create a new object that corresponds to the symbol
                return null;

            }
            throw new SymbolException("Unknown symbol");
        }

        public Object CreateObjectFromNonterminal(NonterminalToken token)
        {
            switch (token.Rule.Id)
            {
                case (int)RuleConstants.RULE_PROGRAM :
                //<Program> ::= <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST :
                //<StatementList> ::= <Statement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENTLIST2 :
                //<StatementList> ::= <Statement> <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT :
                //<Statement> ::= <Assignment>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT2 :
                //<Statement> ::= <IfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_STATEMENT3 :
                //<Statement> ::= <Loop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ASSIGNMENT_IDENTIFIER_EQ :
                //<Assignment> ::= Identifier '=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_IFSTATEMENT_IF_COLON :
                //<IfStatement> ::= if <Expression> ':' <StatementList> <ElseIfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIFSTATEMENT_ELIF_COLON :
                //<ElseIfStatement> ::= elif <Expression> ':' <StatementList> <ElseIfStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIFSTATEMENT :
                //<ElseIfStatement> ::= <ElseStatement>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSEIFSTATEMENT2 :
                //<ElseIfStatement> ::= 
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ELSESTATEMENT_ELSE_COLON :
                //<ElseStatement> ::= else ':' <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP :
                //<Loop> ::= <ForLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_LOOP2 :
                //<Loop> ::= <WhileLoop>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_FORLOOP_FOR_IDENTIFIER_IN_RANGE_LPAREN_INTEGER_RPAREN_COLON :
                //<ForLoop> ::= for Identifier in range '(' Integer ')' ':' <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_WHILELOOP_WHILE_COLON :
                //<WhileLoop> ::= while <Expression> ':' <StatementList>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION :
                //<Expression> ::= <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_GT :
                //<Expression> ::= <Add Exp> '>' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LT :
                //<Expression> ::= <Add Exp> '<' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTEQ :
                //<Expression> ::= <Add Exp> '<=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_GTEQ :
                //<Expression> ::= <Add Exp> '>=' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_EQEQ :
                //<Expression> ::= <Add Exp> '==' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_EXPRESSION_LTGT :
                //<Expression> ::= <Add Exp> '<>' <Expression>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP :
                //<Add Exp> ::= <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_PLUS :
                //<Add Exp> ::= <Mult Exp> '+' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_ADDEXP_MINUS :
                //<Add Exp> ::= <Mult Exp> '-' <Add Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP :
                //<Mult Exp> ::= <Negate Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_TIMES :
                //<Mult Exp> ::= <Negate Exp> '*' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIV :
                //<Mult Exp> ::= <Negate Exp> '/' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_DIVDIV :
                //<Mult Exp> ::= <Negate Exp> '//' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_MULTEXP_PERCENT :
                //<Mult Exp> ::= <Negate Exp> '%' <Mult Exp>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP_MINUS :
                //<Negate Exp> ::= '-' <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_NEGATEEXP :
                //<Negate Exp> ::= <Value>
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_IDENTIFIER :
                //<Value> ::= Identifier
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_INTEGER :
                //<Value> ::= Integer
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_STRINGLITERAL :
                //<Value> ::= StringLiteral
                //todo: Create a new object using the stored tokens.
                return null;

                case (int)RuleConstants.RULE_VALUE_LPAREN_RPAREN :
                //<Value> ::= '(' <Expression> ')'
                //todo: Create a new object using the stored tokens.
                return null;

            }
            throw new RuleException("Unknown rule");
        }

        private void TokenErrorEvent(LALRParser parser, TokenErrorEventArgs args)
        {
            string message = "Token error with input: '"+args.Token.ToString()+"'";
            //todo: Report message to UI?
        }

        private void ParseErrorEvent(LALRParser parser, ParseErrorEventArgs args)
        {
            string message = "Parse error caused by token: '"+args.UnexpectedToken.ToString()+"'";
            //todo: Report message to UI?
            lst1.Items.Add(message);
        }
        private void TokenReadEvent(LALRParser parser, TokenReadEventArgs args)
        {
            String Info = args.Token.Text + "   \t \t" + (SymbolConstants)args.Token.Symbol.Id;
            lst2.Items.Add(Info);
        }

    }
}
