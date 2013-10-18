using System;

struct AnimationHeader
{
    string ImageName;
    int totalRows, totalCols;
}

struct AnimationInfo
{
    string AnimationName;
    int startRow, startCol;
    int endRow, endCol;
    double timeLength;
}