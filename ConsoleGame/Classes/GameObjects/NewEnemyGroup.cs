using System.Diagnostics;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects;

public class NewEnemyGroup
{
    public int StartX;
    public int StartY;
    public readonly int Width;
    public readonly int Height;

    private Position _upperLeftCorner;
    private Position _lowerRightCorner;

    private EnemyDirection _direction;
    private EnemyDirection _nextDirection;
    
    private List<Enemy> _enemies = new();

    public List<Enemy> Enemies
    {
        get => _enemies;
        set => _enemies = value;
    }

    public NewEnemyGroup(int width, int height, int startX, int startY, EnemyDirection direction = EnemyDirection.Right)
    {
        Width = width;
        Height = height;
        StartX = startX;
        StartY = startY;
        
        _direction = direction;
        _upperLeftCorner = (startX, startY);
        _lowerRightCorner = (startX + 4 * width, startY + height * 2);
    }

    public void Move()
    {
        CheckBoundary();

        for (var i = _enemies.Count - 1; i >= 0; i--)
        {
            var enemy = _enemies[i];
            if (!enemy.isActive) _enemies.Remove(enemy);

            enemy.Move(_direction);

            if (enemy.Position.X > _lowerRightCorner.X) _lowerRightCorner.X = enemy.Position.X;
            else if (enemy.Position.X < _upperLeftCorner.X) _upperLeftCorner.X = enemy.Position.X;
        }
    }

    private void CheckBoundary()
    {
        switch (_direction)
        {
            case EnemyDirection.Left when _upperLeftCorner.X < 3:
                _direction = EnemyDirection.Down;
                _nextDirection = EnemyDirection.Right;
                break;
            
            case EnemyDirection.Right when _lowerRightCorner.X > Game.GameScreenWidth - 3:
                _direction = EnemyDirection.Down;
                _nextDirection = EnemyDirection.Left;
                break;
            
            case EnemyDirection.Down:
                _direction = _nextDirection;
                break;
        }
    }
}