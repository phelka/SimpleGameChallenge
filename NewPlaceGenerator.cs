namespace _2024_08_08_Challenge;

public class NewPlaceGenerator
{
    private const int SIZE = 5;
    private string place;

    public NewPlaceGenerator()
    {
        place = "00N";
    }

    private bool IsValidDigit(char c)
    {
        return c >= '0' && c <= '4';
    }

    private bool IsValidLetter(char c)
    {
        return c == 'N' || c == 'E' || c == 'S' || c == 'W';
    }

    private void UpdatePositionBasedOnDirection()
    {
        char direction = place[2];
        int x = int.Parse(place[0].ToString());
        int y = int.Parse(place[1].ToString());

        switch (direction)
        {
            case 'N':
                if (y < SIZE - 1)
                {
                    y += 1;
                }
                break;

            case 'S':
                if (y > 0)
                {
                    y -= 1;
                }
                break;

            case 'E':
                if (x < SIZE - 1)
                {
                    x += 1;
                }
                break;

            case 'W':
                if (x > 0)
                {
                    x -= 1;
                }
                break;

            default:
                throw new InvalidOperationException("Invalid direction in place.");
        }

        place = $"{x:D1}{y:D1}{direction}";
    }

    private void TurnOrCheckMove(char letter)
    {
        if (letter == 'L')
        {
            char currentDirection = place[2];
            char newDirection;

            switch (currentDirection)
            {
                case 'N':
                    newDirection = 'W';
                    break;

                case 'W':
                    newDirection = 'S';
                    break;

                case 'S':
                    newDirection = 'E';
                    break;

                case 'E':
                    newDirection = 'N';
                    break;

                default:
                    newDirection = currentDirection;
                    break;
            }

            place = place.Substring(0, 2) + newDirection;
        }
        else if (letter == 'R')
        {
            char currentDirection = place[2];
            char newDirection;

            switch (currentDirection)
            {
                case 'N':
                    newDirection = 'E';
                    break;

                case 'E':
                    newDirection = 'S';
                    break;

                case 'S':
                    newDirection = 'W';
                    break;

                case 'W':
                    newDirection = 'N';
                    break;

                default:
                    newDirection = currentDirection;
                    break;
            }

            place = place.Substring(0, 2) + newDirection;
        }
        else if (letter == 'M')
        {
            UpdatePositionBasedOnDirection();
        }
    }

    public string GenerateNewPlace(string oldPlace, string input)
    {
        if (oldPlace.Length != 3 ||
            !char.IsDigit(oldPlace[0]) || !char.IsDigit(oldPlace[1]) ||
            !char.IsLetter(oldPlace[2]) ||
            !IsValidDigit(oldPlace[0]) || !IsValidDigit(oldPlace[1]) ||
            !IsValidLetter(oldPlace[2]))
        {
            throw new ArgumentException("Incorrect Starting Place");
        }

        place = oldPlace;

        for (int i = 0; i < input.Length; i++)
        {
            TurnOrCheckMove(input[i]);
        }

        return place;
    }
}