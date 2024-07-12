namespace Journey.Exception.ExceptionsBase;
public class JourneyException : SystemException
    // aqui eu indico que essa classe tem uma herança com SystemException
    // indicando que essa classe é uma classe especial, ela é uma exception
    // dessa maneira, por exemplo, em meus UseCases eu posso utilizar "JourneyException" ao invés de "ArgumentException" 
{
    public JourneyException(string message) : base(message)
        /*
         * eu quero ter essa mensagem como mensagem da exceção
         * SystemException por sua vez tem uma herança direta com Exception
         * e Exception tem uma propriedade string message
         * a idéia é colocar a mensagem de JourneyException nessa propriedade string message.
         * <: base(message)>
         */
    {
            
    }
}
