using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public class EnemyGroup : IRemovable
{
    public readonly int StartX;
    public readonly int StartY;
    public readonly int Width;
    public readonly int Height;
    
    private int _leftGroupEdge;
    private int _rightGroupEdge;
    private int _groupBottom;

    private EnemyDirection _direction;
    private EnemyDirection _nextDirection;

    public readonly List<Enemy> Enemies = new();

    public EnemyGroup(int width, int height, int startX, int startY, EnemyDirection direction = EnemyDirection.Right)
    {
        Width = width;
        Height = height;
        StartX = startX;
        StartY = startY;
        
        _direction = direction;
        
        _leftGroupEdge = startX;
        _rightGroupEdge = startX + (width - 1) * 4;
        _groupBottom = startY + (height - 1) * 2;
    }

    public void Init()
    {
        for (var i = Enemies.Count - 1; i >= 0; i--)
        {
            var enemy = Enemies[i];

            if (enemy.Position.X > _rightGroupEdge) _rightGroupEdge = enemy.Position.X;
            if (enemy.Position.X < _leftGroupEdge) _leftGroupEdge = enemy.Position.X;
            if (enemy.Position.Y > _groupBottom) _groupBottom = enemy.Position.Y;
        }
        
        UpdateAttackingEnemies();
    }

    public void Move()
    {
        CheckBoundary();

        _leftGroupEdge = Game.GameScreenWidth - 1;
        _rightGroupEdge = 0;
        _groupBottom = 0;
        
        var needToUpdateEnemies = false;
        
        for (var i = Enemies.Count - 1; i >= 0; i--)
        {
            var enemy = Enemies[i];
            if (!enemy.IsActive)
            {
                Enemies.Remove(enemy);
                needToUpdateEnemies = true;
                continue;
            }
            
            enemy.Move(_direction);
            
            if (enemy.Position.X > _rightGroupEdge) _rightGroupEdge = enemy.Position.X;
            if (enemy.Position.X < _leftGroupEdge) _leftGroupEdge = enemy.Position.X;
            if (enemy.Position.Y > _groupBottom) _groupBottom = enemy.Position.Y;
        }
        
        if (needToUpdateEnemies) UpdateAttackingEnemies();
    }

    private void CheckBoundary()
    {
        switch (_direction)
        {
            case EnemyDirection.Left when _leftGroupEdge < 3:
                _direction = EnemyDirection.Down;
                _nextDirection = EnemyDirection.Right;
                break;
            
            case EnemyDirection.Right when _rightGroupEdge > Game.GameScreenWidth - 3:
                _direction = EnemyDirection.Down;
                _nextDirection = EnemyDirection.Left;
                break;
            
            case EnemyDirection.Down:
                _direction = _nextDirection;
                break;
        }
    }

    private void UpdateAttackingEnemies()
    {
        if (Enemies.Count == 0)
        {
            Remove();
            return;
        }

        // Jeśli są przeciwnycy w grupie, to conajmniej jeden z nich musi atakować
        while (true)
        {
            var attacking = 0;
            foreach (var enemy in Enemies)
            {
                if (enemy.Position.Y == _groupBottom)
                {
                    enemy.Attacking = true;
                    attacking++;
                }

                else
                {
                    enemy.Attacking = false;
                }
            }

            if (attacking != 0) return;
            _groupBottom--;
        }
    }

    public void Remove()
    {
        ObjectManager.MarkForRemoval(this);
    }
}