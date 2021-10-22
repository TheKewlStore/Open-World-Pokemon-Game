using System;
using UnityEngine;

enum CardinalDirection {
    NORTH,
    SOUTH,
    EAST,
    WEST,
    UNKNOWN
}

static class CardinalDirections {
    public static CardinalDirection getDirectionFromVector(Vector2 direction) {
        if (direction == Vector2.up) {
            return CardinalDirection.NORTH;
        }
        else if (direction == Vector2.left) {
            return CardinalDirection.WEST;
        }
        else if (direction == Vector2.down) {
            return CardinalDirection.SOUTH;
        }
        else if (direction == Vector2.right) {
            return CardinalDirection.EAST;
        }
        else {
            return CardinalDirection.UNKNOWN;
        }
    }

    public static CardinalDirection getDirectionFromString(string direction) {
        if (direction.ToLower() == "east") {
            return CardinalDirection.EAST;
        }
        else if (direction.ToLower() == "west") {
            return CardinalDirection.WEST;
        }
        else if (direction.ToLower() == "north") {
            return CardinalDirection.NORTH;
        }
        else if (direction.ToLower() == "south") {
            return CardinalDirection.SOUTH;
        }
        else {
            return CardinalDirection.UNKNOWN;
        }
    }

    public static string directionName(CardinalDirection direction) {
        switch (direction) {
            case CardinalDirection.NORTH:
                return "north";
            case CardinalDirection.SOUTH:
                return "south";
            case CardinalDirection.EAST:
                return "east";
            case CardinalDirection.WEST:
                return "west";
            case CardinalDirection.UNKNOWN:
            default:
                return "unknown";
        }
    }
}
