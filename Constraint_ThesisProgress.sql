alter table [dbo].[ThesisProgresses]
ADD CONSTRAINT [FK_dbo.ThesisProgresses_dbo.ThesisForms_ThesisForm_ID] FOREIGN KEY ([ThesisForm_ID]) REFERENCES [dbo].[ThesisForms] ([ID]) on delete cascade;

