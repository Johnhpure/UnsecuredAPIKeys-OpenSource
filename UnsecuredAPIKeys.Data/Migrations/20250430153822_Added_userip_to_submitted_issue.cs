﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UnsecuredAPIKeys.Data.Migrations
{
    /// <inheritdoc />
    public partial class Added_userip_to_submitted_issue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIP",
                table: "IssueSubmissionTrackings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIP",
                table: "IssueSubmissionTrackings");
        }
    }
}
