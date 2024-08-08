namespace _2024_08_08_Challenge;

public class NewPlaceGenerator // TODO keep it short: PlaceGenerator (new seems to be not needed)
{
    private const int SIZE = 5;
    private string place;

    public NewPlaceGenerator()
    {
        place = "0 0 N"; // TODO place could be a structure or a class
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
        char direction = place[4]; // TODO this would be easier if place would be a struct or a class - e.g. Place or Location; also direction could be an Enum
        int x = int.Parse(place[0].ToString());
        int y = int.Parse(place[2].ToString());

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


        place = $"{x} {y} {direction}";
    }

    private void TurnOrCheckMove(char letter) // TODO `letter` param could have a better name - like `command`; also it could be an Enum
    {
        if (letter == 'L')
        {
            char currentDirection = place[4];
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

            place = $"{place[0]} {place[2]} {newDirection}";
        }
        else if (letter == 'R')
        {
            char currentDirection = place[4];
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

            place = $"{place[0]} {place[2]} {newDirection}";
        }
        else if (letter == 'M')
        {
            UpdatePositionBasedOnDirection();
        }
    }

    public string GenerateNewPlace(string oldPlace, string input) // TODO separation of concern: one thing is to parse input text from user, another thing is to apply moves/commands to the pawn. Separate to two classes maybe?
    {
        if (oldPlace.Length != 5 ||
            !char.IsDigit(oldPlace[0]) || !char.IsDigit(oldPlace[2]) ||
            !char.IsLetter(oldPlace[4]) ||
            !IsValidDigit(oldPlace[0]) || !IsValidDigit(oldPlace[2]) ||
            !IsValidLetter(oldPlace[4]))
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
