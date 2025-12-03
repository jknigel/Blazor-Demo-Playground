#1 Identify Key Categories for Library (Tables)
- Members
- Books
- Loans

#2 Columns for each Table

Members Table:
- MemberID (Primary KEY)
- Name
- Email

Books Table:
- BookID (Primary KEY)
- ISBN (Unique)
- Author

Loans Table:
- LoanID (Primary KEY)
- MemberID (FK)
- BookID (FK)
- Loan Date
- Return Date


